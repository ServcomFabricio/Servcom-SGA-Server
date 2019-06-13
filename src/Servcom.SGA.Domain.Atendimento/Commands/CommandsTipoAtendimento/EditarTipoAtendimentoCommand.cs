using Servcom.SGA.Domain.Core.Commands;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento
{
    public class EditarTipoAtendimentoCommand:Command
    {
        public EditarTipoAtendimentoCommand(Guid id, string tipo, string descricao, bool prioritario)
        {
            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            Prioritario = prioritario;
        }

        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public bool Prioritario { get; set; }
    }
}
