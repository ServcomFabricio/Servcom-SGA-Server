using MediatR;
using Servcom.SGA.Domain.Atendimentos.Events.EventsTipoAtendimento;
using Servcom.SGA.Domain.Atendimentos.Repository;
using Servcom.SGA.Domain.Core.Handlers;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento
{
    public class TipoAtendimentoCommandHandler : CommandHandler,
        IRequestHandler<IncluirTipoAtendimentoCommand,bool>,
        IRequestHandler<EditarTipoAtendimentoCommand, bool>,
        IRequestHandler<ExcluirTipoAtendimentoCommand, bool>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IUser _user;
        private readonly ITipoAtendimentoRepository _tipoAtendimentoRepository;
        public TipoAtendimentoCommandHandler(IUnitOfWork uow,
                                             IMediatorHandler mediator,
                                             INotificationHandler<DomainNotification> notifications,
                                             IUser user,
                                             ITipoAtendimentoRepository tipoAtendimentoRepository)
                                            : base(uow, mediator, notifications)
        {
            _tipoAtendimentoRepository = tipoAtendimentoRepository;
            _user = user;
            _mediator = mediator;
        }

        public Task<bool> Handle(IncluirTipoAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var tipoAtendimento = new TipoAtendimento(request.Id, request.Tipo, request.Descricao, request.Prioritario);

            if (!tipoAtendimento.EhValido())
            {
                NotificarValidacoesErro(tipoAtendimento.ValidationResult);
                return Task.FromResult(true);
            }

            var tipoAtendimentoExiste = _tipoAtendimentoRepository.Buscar(o => o.Tipo == tipoAtendimento.Tipo);
            if (tipoAtendimentoExiste.Any())
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Tipo de Atendimento já utilizado"));
                return Task.FromResult(true);
            }

            _tipoAtendimentoRepository.Registrar(tipoAtendimento);

            if (Commit())
            {
                _mediator.PublicarEvento(new TipoAtendimentoRegistradoEvent(tipoAtendimento.Id, tipoAtendimento.Tipo, tipoAtendimento.Descricao, tipoAtendimento.Prioritario));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(EditarTipoAtendimentoCommand request, CancellationToken cancellationToken)
        {
            if (!tipoAtendimentoExistente(request.Id, request.MessageType)) return Task.FromResult(true);

            var tipoAtendimento = new TipoAtendimento(request.Id, request.Tipo, request.Descricao, request.Prioritario);

            if (!tipoAtendimento.EhValido())
            {
                NotificarValidacoesErro(tipoAtendimento.ValidationResult);
                return Task.FromResult(true);
            }

            var tipoAtendimentoEditado = _tipoAtendimentoRepository.ObterPorId(tipoAtendimento.Id);
            tipoAtendimento.setStatusTipo(tipoAtendimentoEditado.Ativo);
            if (tipoAtendimentoEditado.Tipo != tipoAtendimento.Tipo)
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Tipo não pode ser alterado"));
            }

            _tipoAtendimentoRepository.Atualizar(tipoAtendimento);
            var com = Commit();
            if (com)
            {
                _mediator.PublicarEvento(new TipoAtendimentoAtualizadoEvent(tipoAtendimento.Id, tipoAtendimento.Tipo, tipoAtendimento.Descricao, tipoAtendimento.Prioritario));
            }
            return Task.FromResult(true);
        }

        public Task<bool> Handle(ExcluirTipoAtendimentoCommand request, CancellationToken cancellationToken)
        {
            if (!tipoAtendimentoExistente(request.Id, request.MessageType)) return Task.FromResult(true);

            // Validacoes de negocio

            var tipoAtendimentoExcluir = _tipoAtendimentoRepository.ObterPorId(request.Id);
            tipoAtendimentoExcluir.setStatusTipo(false);

            _tipoAtendimentoRepository.Atualizar(tipoAtendimentoExcluir);


            if (Commit())
            {
                _mediator.PublicarEvento(new TipoAtendimentoExcluidoEvent(tipoAtendimentoExcluir.Id));
            }

            return Task.FromResult(true);
        }

        private bool tipoAtendimentoExistente(Guid id, string messageType)
        {
            var tipoAtendimento = _tipoAtendimentoRepository.ObterPorId(id);

            if (tipoAtendimento != null) return true;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Tipo atendimento não encontrado."));
            return false;
        }
    }
}
