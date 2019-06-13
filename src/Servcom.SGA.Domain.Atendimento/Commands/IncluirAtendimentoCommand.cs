using System;

namespace Servcom.SGA.Domain.Atendimentos.Commands
{
    public class IncluirAtendimentoCommand:BaseAtendimentoCommand
    {
        public IncluirAtendimentoCommand(Guid id,Guid? tipoId)
        {
            Id = id;
            TipoId = tipoId;
            
        }
    }
}
