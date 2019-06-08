using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Usuarios.Events
{
    public class UsuarioEventHandler : 
        INotificationHandler<UsuarioRegistradoEvent>,
         INotificationHandler<UsuarioExcluidoEvent>,
        INotificationHandler<UsuarioAtualizadoEvent>
    {
        public Task Handle(UsuarioRegistradoEvent notification, CancellationToken cancellationToken)
        {
            //fazer algo depois do registro
            return Task.CompletedTask;
        }

        public Task Handle(UsuarioExcluidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(UsuarioAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
