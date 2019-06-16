using Servcom.SGA.Domain.Core.Events;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Events
{
    public class AtendimentoProximoSolicitadoEvent: BaseAtendimentoEvent
    {
        public AtendimentoProximoSolicitadoEvent(Guid id,DateTime dataHoraChamada, Guid? usuarioId, string guiche,string horaCriacao)
        {
            Id = id;
            DataHoraChamada = dataHoraChamada;
            UsuarioId = usuarioId;
            Guiche = guiche;
            HoraCriacao = horaCriacao;

            AggregateId = id;
        }
    }
}
