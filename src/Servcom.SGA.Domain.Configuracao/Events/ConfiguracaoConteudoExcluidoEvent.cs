using Servcom.SGA.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servcom.SGA.Domain.Configuracao.Events
{
    public class ConfiguracaoConteudoExcluidoEvent:Event
    {
        public ConfiguracaoConteudoExcluidoEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
