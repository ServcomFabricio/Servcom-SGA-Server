using System;

namespace Servcom.SGA.Domain.Atendimentos.Events
{
    public class AtendimentoRegistradoEvent:BaseAtendimentoEvent
    {
        public AtendimentoRegistradoEvent(Guid id,int sequencia, DateTime dataCriacao, Guid? tipoId,string horaCriacao)
        {
            Id = id;
            Sequencia = sequencia;
            TipoId = tipoId;
            DataCriacao = dataCriacao;
            HoraCriacao = horaCriacao;

            AggregateId = id;
        }
    }
}
