using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Configuracao.Events
{
    public class ConfiguracaoGeralEventHandler :
        INotificationHandler<ConfiguracaoGeralAtualizadoEvent>

    {
        public Task Handle(ConfiguracaoGeralAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
