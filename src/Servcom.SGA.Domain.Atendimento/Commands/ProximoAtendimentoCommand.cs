using Servcom.SGA.Domain.Core.Commands;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Commands
{
    public class ProximoAtendimentoCommand : CommandEntity
    {
        public ProximoAtendimentoCommand(string tipo, DateTime dataCriacao, string usuario, bool? prioritario = false)
        {
            Tipo = tipo;
            DataCriacao = dataCriacao;
            Usuario = usuario;
            Prioritario = (bool)prioritario;

        }

        public string Tipo { get; set; }
        public string Usuario { get; set; }
        public Guid Id { get;  set; }
        public int Sequencia { get;  set; }
        public Guid? TipoId { get;  set; }
        public DateTime DataCriacao { get;  set; }
        public string HoraCriacao { get;  set; }
        public DateTime DataHoraInicio { get;  set; }
        public DateTime DataHoraFim { get;  set; }
        public DateTime DataHoraultimoReingresso { get;  set; }
        public EStatus Status { get;  set; }
        public Guid? UsuarioId { get;  set; }
        public DateTime DataHoraChamada { get;  set; }
        public string Guiche { get;  set; }
        public bool Prioritario { get;  set; }
        public string Senha { get;  set; }


    }
}
