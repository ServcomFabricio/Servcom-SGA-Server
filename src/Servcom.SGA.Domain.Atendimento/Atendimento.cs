using Servcom.SGA.Domain.Core.Models;
using System;

namespace Servcom.SGA.Domain.Atendimentos
{
    public class Atendimento : Entity<Atendimento>
    {
        public Atendimento(Guid id, bool prioritario, Guid? tipoId)
        {
            Id = id;
            TipoId = tipoId;
            DataCriacao = DateTime.Now.Date;
            HoraCriacao = DateTime.Now.ToString("HH:mm");
            Status = EStatus.Ativo;
            Prioritario = prioritario;

        }

        private Atendimento() { }

        public int Sequencia { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string HoraCriacao { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get; private set; }
        public DateTime DataHoraultimoReingresso { get; private set; }
        public DateTime DataHoraChamada { get; private set; }
        public string Guiche { get; private set; }
        public bool Prioritario { get; private set; }
        public EStatus Status { get; private set; }
        public Guid? UsuarioId { get; private set; }
        public Guid? TipoId { get; private set; }
        public string Senha { get; private set; }
        public DateTime TimeStamp { get; private set; }


        //EF propriedade de navegação
        public virtual TipoAtendimento TipoAtendimento { get; private set; }

        public void SetSequencia(int sequencia, string tipo,bool prioridade)
        {
            tipo = prioridade == true ? tipo + "P" : tipo;
            Sequencia = sequencia;
            Senha = tipo + String.Format("{0:D4}", Sequencia);
        }

        public void SetNovoAtendimento(Guid? usuarioId, string guiche)
        {
            UsuarioId = usuarioId;
            Guiche = guiche;
            DataHoraChamada = DateTime.Now;
        }


        public override bool EhValido()
        {
            return true;
        }

        public static class AtendimentoFactory
        {
            public static Atendimento EditarAtendimento(Guid id, DateTime dataHoraInicio, DateTime dataHoraFim, DateTime dataHoraultimoReingresso, string guiche, EStatus status, Guid? usuarioId, Guid? tipoId, int sequencia, DateTime dataCriacao,DateTime timeStamp)
            {
                usuarioId = usuarioId == Guid.Empty ? null : usuarioId;
                return new Atendimento()
                {
                    Id = id,
                    DataHoraInicio = dataHoraInicio,
                    DataHoraFim = dataHoraFim,
                    DataHoraultimoReingresso = dataHoraultimoReingresso,
                    Guiche = guiche,
                    Status = status,
                    UsuarioId = usuarioId,

                    TipoId=tipoId,
                    Sequencia=sequencia,
                    DataCriacao=dataCriacao,
                    TimeStamp=timeStamp
                    
            };

        }
    }

}
}
