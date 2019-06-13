using Servcom.SGA.Domain.Core.Commands;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento
{
    public class ExcluirTipoAtendimentoCommand:Command
    {
        public ExcluirTipoAtendimentoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }
}
