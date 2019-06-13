using Servcom.SGA.Domain.Core.Models;
using System;

namespace Servcom.SGA.Domain.Atendimentos
{
    public class Atendimento:Entity<Atendimento>
    {
        public Atendimento(Guid id, Guid? tipoId)
        {
            Id = id;
            TipoId = tipoId;
            DataCriacao = DateTime.Now.Date;
            HoraCriacao = DateTime.Now.ToString("HH:mm");
            Status = EStatus.Ativo;
            

        }

        private Atendimento() {}

        public int Sequencia { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string HoraCriacao { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get; private set; }
        public DateTime DataHoraultimoReingresso { get; private set; }
        public EStatus Status { get; private set; }
        public Guid? UsuarioId { get; private set; }
        public Guid? TipoId { get; private set; }
        public string Senha { get; private set; }
        public DateTime TimeStamp { get; private set; }


        //EF propriedade de navegação
        public virtual TipoAtendimento TipoAtendimento { get; private set; }

        public void setSequencia(int sequencia)
        {
            Sequencia = sequencia;
        }

        public void setUsuario(Guid? usuarioId)
        {
            UsuarioId = usuarioId;
        }

        public void setSenha(string tipo)
        {
            Senha = tipo + String.Format("{0:D4}", Sequencia);
        }


        public override bool EhValido()
        {
            return true;
        }

      

    }
}
