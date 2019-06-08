using System;

namespace Servcom.SGA.Domain.Atendimentos.Events
{
    public class AtendimentoRegistradoEvent:BaseAtendimentoEvent
    {
        public AtendimentoRegistradoEvent(Guid id,int senha, Guid tipoId, DateTime dataHoraCriacao)
        {
            Id = id;
            Senha = senha;
            TipoId = tipoId;
            DataHoraCriacao = dataHoraCriacao;

            AggregateId = id;
        }
    }
}
