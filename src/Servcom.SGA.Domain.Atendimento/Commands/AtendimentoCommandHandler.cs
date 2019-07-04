using MediatR;
using Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento;
using Servcom.SGA.Domain.Atendimentos.Events;
using Servcom.SGA.Domain.Atendimentos.Repository;
using Servcom.SGA.Domain.Core.Handlers;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using Servcom.SGA.Domain.Usuarios.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Atendimentos.Commands
{
    public class AtendimentoCommandHandler : CommandHandler,
        IRequestHandler<IncluirAtendimentoCommand, object>,
        IRequestHandler<ProximoAtendimentoCommand, object>,
        IRequestHandler<EditarAtendimentoCommand, bool>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly ITipoAtendimentoRepository _tipoAtendimentoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly DomainNotificationHandler _notifications;
        public AtendimentoCommandHandler(IUnitOfWork uow,
                                         IMediatorHandler mediator,
                                         IAtendimentoRepository atendimentoRepository,
                                         ITipoAtendimentoRepository tipoAtendimentoRepository,
                                         IUsuarioRepository usuarioRepository,
                                         INotificationHandler<DomainNotification> notifications)
                                         : base(uow, mediator, notifications)
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
            _atendimentoRepository = atendimentoRepository;
            _tipoAtendimentoRepository = tipoAtendimentoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<object> Handle(IncluirAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var atendimento = new Atendimento(request.Id, request.Prioritario, request.TipoId);

            if (!atendimento.EhValido())
            {
                NotificarValidacoesErro(atendimento.ValidationResult);
                return null;
            }
            var tipo = _tipoAtendimentoRepository.ObterPorId((Guid)request.TipoId);

            if (tipo==null)
            {
                await _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Tipo de atendimento não encontrado"));
                return null;
            }
            var seq = (int)_atendimentoRepository.ObterUltimoAtendimento(atendimento.TipoId, atendimento.DataCriacao);
            atendimento.SetSequencia(seq + 1,tipo.Tipo,atendimento.Prioritario);

            _atendimentoRepository.Registrar(atendimento);

            if (_atendimentoRepository.SaveChangesIncluir(atendimento) > 0)
            {
                await _mediator.PublicarEvento(new AtendimentoRegistradoEvent(atendimento.Id, atendimento.Sequencia, atendimento.DataCriacao, atendimento.TipoId, atendimento.HoraCriacao));
            }
            return await Task.FromResult(atendimento);
        }

        public async Task<object> Handle(ProximoAtendimentoCommand request, CancellationToken cancellationToken)
        {

            var tipo = _tipoAtendimentoRepository.Buscar(t => t.Tipo == request.Tipo);

            if (!tipo.Any())
            {
                await _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Tipo de atendimento não encontrado"));
                return null;
            }

            var usuario = _usuarioRepository.Buscar(u => u.Sigla == request.Usuario);

            if (!usuario.Any())
            {
                await _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Usuário não encontrado"));
                return null;
            }

            var tipoId = tipo.FirstOrDefault().Id;
            var usuarioId = usuario.FirstOrDefault().Id;
            var atendimento = _atendimentoRepository.ObterPrimeiroAtendimentoSemUsuario((Guid?)tipoId, request.DataCriacao, (Guid?)usuarioId, request.Prioritario);

            if (atendimento == null)
            {
                await _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Nenhum atendimento encontrado"));
                return null;
            }

            atendimento.SetNovoAtendimento(usuarioId, request.Guiche);

            _atendimentoRepository.Atualizar(atendimento);

            if (_notifications.HasNotifications()) return null;

            var result = _atendimentoRepository.SaveChangesUsuario(atendimento);
            var qtdSave = result.Item1;
            atendimento = result.Item2;

            if (qtdSave > 0)
            {
                await _mediator.PublicarEvento(new AtendimentoProximoSolicitadoEvent(atendimento.Id, atendimento.DataHoraChamada, atendimento.UsuarioId, atendimento.Guiche, atendimento.HoraCriacao));
                return await Task.FromResult(atendimento);
            }
            await _mediator.PublicarEvento(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados no banco"));
            return null;
        }

        public Task<bool> Handle(EditarAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var atendimentoEditado = _atendimentoRepository.ObterPorId(request.Id);
            if (atendimentoEditado == null)
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Atendimento não encontrado"));
                return Task.FromResult(true);
            }

            var atendimento = Atendimento.AtendimentoFactory.EditarAtendimento(request.Id, request.DataHoraInicio, request.DataHoraFim, request.DataHoraultimoReingresso, request.Guiche, request.Status, request.UsuarioId, atendimentoEditado.TipoId, atendimentoEditado.Sequencia, atendimentoEditado.DataCriacao,atendimentoEditado.TimeStamp);
            var noUpdate = new[] { "TipoId", "DataCriacao","HoraCriacao", "Prioritario", "Sequencia", "Senha" };

            if (!atendimento.EhValido())
            {
                NotificarValidacoesErro(atendimento.ValidationResult);
                return Task.FromResult(true);
            }

            _atendimentoRepository.Atualizar(atendimento, noUpdate);

            if (Commit())
            {
                _mediator.PublicarEvento(new AtendimentoAtualizadoEvent(atendimento.Id,atendimento.DataHoraInicio,atendimento.DataHoraFim,atendimento.DataHoraultimoReingresso,atendimento.Guiche,atendimento.Status,atendimento.UsuarioId));
            }
            return Task.FromResult(true);
        }
    }
}
