using Servcom.SGA.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servcom.SGA.Domain.Configuracao.Commands
{
    public class ExcluirConfiguracaoConteudoCommand:Command
    {
        public ExcluirConfiguracaoConteudoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
