using Servcom.SGA.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Servcom.SGA.Domain.Atendimentos
{
    public class TipoAtendimento:Entity<TipoAtendimento> 
    {
        public TipoAtendimento(Guid id,string tipo, string descricao, bool prioritario)
        {
            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            Prioritario = prioritario;
            Ativo = true;
        }

        private TipoAtendimento() { }

        public string Tipo { get; private set; }
        public string Descricao { get; private set; }
        public bool Prioritario { get; private set; }
        public bool Ativo { get; private set; }

        public void setStatusTipo(bool status) {
            Ativo = status;
        }

        // EF Propriedade de Navegação
        public virtual ICollection<Atendimento> Atendimentos { get; set; }

        public override bool EhValido()
        {
            return true;
        }
    }
}
