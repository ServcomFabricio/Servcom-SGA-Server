using Servcom.SGA.Domain.Atendimentos;
using System;

namespace Servcom.SGA.Service.Api.ViewModels.Atendimento
{
    public class AtendimentoViewModel
    {
        public AtendimentoViewModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public int Sequencia { get;  set; }
        public DateTime DataCriacao { get;  set; }
        public string HoraCriacao { get;  set; }
        public DateTime DataHoraInicio { get;  set; }
        public DateTime DataHoraFim { get;  set; }
        public DateTime DataHoraultimoReingresso { get;  set; }
        public EStatus Status { get;  set; }
        public Guid UsuarioId { get;  set; }
        public Guid TipoId { get;  set; }
        public string Senha { get;  set; }

    }
}
