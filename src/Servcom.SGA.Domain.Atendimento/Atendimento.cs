using Servcom.SGA.Domain.Core.Models;
using System;


namespace Servcom.SGA.Domain.Atendimentos
{
    public class Atendimento:Entity<Atendimento>
    {
        public Atendimento(Guid id, Guid? tipoId, DateTime dataCriacao)
        {
            Id = id;
            TipoId = tipoId;
            DataCriacao = dataCriacao;
        }

        private Atendimento() {}

        public DateTime DataCriacao { get; private set; }
        public DateTime HoraCriacao { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get; private set; }
        public DateTime DataHoraultimoReingresso { get; private set; }
        public EStatus Status { get; private set; }
        public Guid? UsuarioId { get; private set; }
        public Guid? TipoId { get; private set; }

        //EF propriedade de navegação
        public virtual string Usuario { get; private set; }
        public virtual TipoAtendimento TipoAtendimento { get; private set; }

        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
      
    }
}
