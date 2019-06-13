using Servcom.SGA.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Servcom.SGA.Domain.Usuarios
{
    public class Usuario : Entity<Usuario>
    {
        public Usuario(Guid id, string sigla, string nome, string setor)
        {
            Id = id;
            Sigla = sigla;
            Nome = nome;
            Setor = setor;
        }
        public string Sigla { get; private set; }
        public string Nome { get; private set; }
        public string Setor { get; private set; }

        // EF Construtor
        private Usuario() { }

        public override bool EhValido()
        {
            return true;
        }
    }
}
