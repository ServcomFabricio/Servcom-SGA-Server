using Servcom.SGA.Domain.Core.Events;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Events.EventsTipoAtendimento
{
    public class TipoAtendimentoExcluidoEvent:Event
    {
        public TipoAtendimentoExcluidoEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
