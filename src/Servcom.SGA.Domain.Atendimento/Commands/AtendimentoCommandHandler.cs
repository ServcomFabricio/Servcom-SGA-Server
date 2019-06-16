using MediatR;
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
        IRequestHandler<IncluirAtendimentoCommand, bool>,
        IRequestHandler<ProximoAtendimentoCommand, object>
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

        public Task<bool> Handle(IncluirAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var atendimento = new Atendimento(request.Id, request.TipoId);

            if (!atendimento.EhValido())
            {
                NotificarValidacoesErro(atendimento.ValidationResult);
                return Task.FromResult(true);
            }
            var tipo = _tipoAtendimentoRepository.ObterPorId((Guid)request.TipoId);

            if (tipo==null)
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Tipo de atendimento não encontrado"));
                return Task.FromResult(true);
            }
            var seq = (int)_atendimentoRepository.obterUltimoAtendimento(atendimento.TipoId, atendimento.DataCriacao);
            atendimento.setSequencia(seq + 1,tipo.Tipo);

            _atendimentoRepository.Registrar(atendimento);

            if (_atendimentoRepository.SaveChangesIncluir(atendimento) > 0)
            {
                _mediator.PublicarEvento(new AtendimentoRegistradoEvent(atendimento.Id, atendimento.Sequencia, atendimento.DataCriacao, atendimento.TipoId, atendimento.HoraCriacao));
            }
            return Task.FromResult(true);
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
            var atendimento = _atendimentoRepository.obterPrimeiroAtendimentoSemUsuario((Guid?)tipoId, request.DataCriacao, (Guid?)usuarioId, request.Prioritario);

            if (atendimento == null)
            {
                await _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Nenhum atendimento encontrado"));
                return null;
            }

            atendimento.setNovoAtendimento(usuarioId, request.Guiche);

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
    }
}
