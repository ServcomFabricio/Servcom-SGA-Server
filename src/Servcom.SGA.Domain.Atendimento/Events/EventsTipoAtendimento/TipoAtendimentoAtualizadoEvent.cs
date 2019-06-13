using Servcom.SGA.Domain.Core.Events;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Events.EventsTipoAtendimento
{
    public class TipoAtendimentoAtualizadoEvent:Event
    {
        public TipoAtendimentoAtualizadoEvent(Guid id, string tipo, string descricao, bool prioritario)
        {
            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            Prioritario = prioritario;
        }

        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public bool Prioritario { get; set; }
    }
}
