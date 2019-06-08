using MediatR;
using Servcom.SGA.Domain.Core.Menssages;
using System;

namespace Servcom.SGA.Domain.Core.Commands
{
    public class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
