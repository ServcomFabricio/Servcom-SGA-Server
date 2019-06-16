using MediatR;
using Servcom.SGA.Domain.Core.Commands;
using Servcom.SGA.Domain.Core.Events;
using Servcom.SGA.Domain.Core.Interfaces;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Core.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;

        }

        public async Task EnviarComando<T>(T comando) where T : Command
        {
            await _mediator.Send(comando);
        }

        public async Task<object> EnviarComandoEntity<T>(T comando) where T : CommandEntity
        {
            return await Task.FromResult(await _mediator.Send(comando));
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {

            await _mediator.Publish(evento);
        }


    }
}
