using Servcom.SGA.Domain.Core.Commands;
using System;

namespace Servcom.SGA.Domain.Usuarios.Commands
{
    public class ExcluirUsuarioCommand:Command
    {
        public ExcluirUsuarioCommand(Guid id)
        {
            Id = id;
            AggregateId = id;

        }
        public Guid Id { get; set; }

    }
}
