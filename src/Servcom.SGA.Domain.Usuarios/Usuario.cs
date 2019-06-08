using Servcom.SGA.Domain.Core.Models;
using Servcom.SGA.Domain.Atendimentos;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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
        protected Usuario() { }

        // EF Propriedade de Navegação
        public virtual ICollection<Atendimento> Atendimento { get; set; }

        public override bool EhValido()
        {
            return true;
        }
    }
}
