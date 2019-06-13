using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Atendimentos.Events.EventsTipoAtendimento
{
    public class TipoAtendimentoEventHandler :
        INotificationHandler<TipoAtendimentoRegistradoEvent>,
        INotificationHandler<TipoAtendimentoAtualizadoEvent>,
        INotificationHandler<TipoAtendimentoExcluidoEvent>
    {
        public Task Handle(TipoAtendimentoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(TipoAtendimentoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(TipoAtendimentoExcluidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
