using Servcom.SGA.Domain.Core.Commands;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Commands
{
    public abstract class BaseAtendimentoCommand:Command
    {
        public Guid Id { get; protected set; }
        public int Sequencia { get; protected set; }
        public Guid? TipoId { get; protected set; }
        public DateTime DataHoraCriacao { get; protected set; }
        public DateTime DataHoraInicio { get; protected set; }
        public DateTime DataHoraFim { get; protected set; }
        public DateTime DataHoraultimoReingresso { get; protected set; }
        public EStatus Status { get; protected set; }
        public Guid? UsuarioId { get; protected set; }

    }
}
