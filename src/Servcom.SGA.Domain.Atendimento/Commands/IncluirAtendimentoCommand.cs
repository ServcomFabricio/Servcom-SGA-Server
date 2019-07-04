using Servcom.SGA.Domain.Core.Commands;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Commands
{
    public class IncluirAtendimentoCommand: CommandEntity
    {
        public IncluirAtendimentoCommand(Guid id,bool prioritario,Guid? tipoId)
        {
            Id = id;
            TipoId = tipoId;
            Prioritario = prioritario;
        }
        public Guid? TipoId { get; set; }
        public Guid Id { get;  set; }
        public bool Prioritario { get; set; }

    }
}
