using MediatR;
using Servcom.SGA.Domain.Core.Handlers;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Atendimentos.Commands
{
    public class AtendimentoCommandHandler : CommandHandler,
        IRequestHandler<RegistrarAtendimentoCommand,bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;
        public AtendimentoCommandHandler(IUnitOfWork uow,
                                         IMediatorHandler mediator,                 
                                         INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public Task<bool> Handle(RegistrarAtendimentoCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
