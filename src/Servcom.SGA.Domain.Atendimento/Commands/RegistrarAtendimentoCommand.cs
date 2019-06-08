using System;

namespace Servcom.SGA.Domain.Atendimentos.Commands
{
    public class RegistrarAtendimentoCommand:BaseAtendimentoCommand
    {
        public RegistrarAtendimentoCommand(int tipoId, DateTime dataHoraCriacao)
        {
            TipoId = tipoId;
            DataHoraCriacao = dataHoraCriacao;
        }
    }
}
