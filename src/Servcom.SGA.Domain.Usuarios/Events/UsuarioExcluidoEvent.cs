using Servcom.SGA.Domain.Core.Events;
using System;

namespace Servcom.SGA.Domain.Usuarios.Events
{
    public class UsuarioExcluidoEvent:Event
    {
        public UsuarioExcluidoEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id { get; set; }
    }
}
