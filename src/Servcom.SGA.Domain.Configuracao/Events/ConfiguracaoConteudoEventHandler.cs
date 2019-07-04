using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Configuracao.Events
{
    public class ConfiguracaoConteudoEventHandler :
        INotificationHandler<ConfiguracaoConteudoRegistradoEvent>,
        INotificationHandler<ConfiguracaoConteudoAtualizadoEvent>,
        INotificationHandler<ConfiguracaoConteudoExcluidoEvent>
    {
        public Task Handle(ConfiguracaoConteudoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ConfiguracaoConteudoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;

        }

        public Task Handle(ConfiguracaoConteudoExcluidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;

        }
    }
}
