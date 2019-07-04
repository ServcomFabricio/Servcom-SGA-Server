using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Atendimentos.Events
{
    public class AtendimentoEventHandler :
        INotificationHandler<AtendimentoRegistradoEvent>,
        INotificationHandler<AtendimentoProximoSolicitadoEvent>,
        INotificationHandler<AtendimentoAtualizadoEvent>
    {
        public Task Handle(AtendimentoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(AtendimentoProximoSolicitadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(AtendimentoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
