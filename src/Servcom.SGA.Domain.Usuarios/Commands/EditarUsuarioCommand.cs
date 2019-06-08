using Servcom.SGA.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servcom.SGA.Domain.Usuarios.Commands
{
    public class EditarUsuarioCommand:Command
    {
        public EditarUsuarioCommand(Guid id, string sigla, string nome, string setor)
        {
            Id = id;
            Sigla = sigla;
            Nome = nome;
            Setor = setor;
        }

        public Guid Id { get; private set; }
        public string Sigla { get; private set; }
        public string Nome { get; private set; }
        public string Setor { get; private set; }
    }
}
