using System;

namespace Servcom.SGA.Domain.Atendimentos.Events
{
    public class AtendimentoRegistradoEvent:BaseAtendimentoEvent
    {
        public AtendimentoRegistradoEvent(Guid id,int sequencia, DateTime dataHoraCriacao, Guid? tipoId)
        {
            Id = id;
            Sequencia = sequencia;
            TipoId = tipoId;
            DataHoraCriacao = dataHoraCriacao;

            AggregateId = id;
        }
    }
}
