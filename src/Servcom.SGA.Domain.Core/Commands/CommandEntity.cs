using MediatR;
using Servcom.SGA.Domain.Core.Menssages;
using System;

namespace Servcom.SGA.Domain.Core.Commands
{
    public class CommandEntity : Message, IRequest<object>
    {
        public DateTime Timestamp { get; private set; }

        public CommandEntity()
        {
            Timestamp = DateTime.Now;
        }
    }
}
