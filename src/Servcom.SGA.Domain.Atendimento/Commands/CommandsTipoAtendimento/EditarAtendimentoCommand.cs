using System;

namespace Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento
{
    public class EditarAtendimentoCommand : BaseAtendimentoCommand
    {
        public EditarAtendimentoCommand(Guid id, DateTime dataHoraInicio, DateTime dataHoraFim, DateTime dataHoraultimoReingresso, string guiche, EStatus status, Guid? usuarioId)
        {
            Id = id;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            DataHoraultimoReingresso = dataHoraultimoReingresso;
            Guiche = guiche;
            Status = status;
            UsuarioId = usuarioId;
        }
    }
}
