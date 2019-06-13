using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Atendimentos.Events
{
    public class AtendimentoEventHandler :
        INotificationHandler<AtendimentoRegistradoEvent>
    {
        public Task Handle(AtendimentoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
