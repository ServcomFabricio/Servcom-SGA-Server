using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using System.Linq;

namespace Servcom.SGA.Infra.CrossCutting.AspNetFilters
{
    public class ValidateModelAttribute : IResultFilter
    {
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        public ValidateModelAttribute(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;

        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                NotificarErroModelInvalida(context);
                context.Result = Response();
                return;
            }
        }

        public void NotificarErroModelInvalida(ResultExecutingContext context)
        {
            var erros = context.ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                _mediator.PublicarEvento(new DomainNotification(string.Empty, erroMsg));
                ;
            }
        }

        private BadRequestObjectResult Response(object result = null)
        {
            return new BadRequestObjectResult(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }
    }
}
