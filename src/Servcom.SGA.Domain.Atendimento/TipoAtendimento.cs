using Servcom.SGA.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Servcom.SGA.Domain.Atendimentos
{
    public class TipoAtendimento:Entity<TipoAtendimento> 
    {
        public TipoAtendimento(Guid id)
        {
            Id = id;
        }
        private TipoAtendimento() { }

        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public Boolean Prioritario { get; set; }

        // EF Propriedade de Navegação
        public virtual ICollection<Atendimento> Atendimentos { get; set; }

        public override bool EhValido()
        {
            return true;
        }
    }
}
