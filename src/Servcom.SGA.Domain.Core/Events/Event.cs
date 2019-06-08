using MediatR;
using Servcom.SGA.Domain.Core.Menssages;
using System;

namespace Servcom.SGA.Domain.Core.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
