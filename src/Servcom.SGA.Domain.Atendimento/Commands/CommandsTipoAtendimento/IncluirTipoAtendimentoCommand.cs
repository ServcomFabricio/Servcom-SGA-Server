using Servcom.SGA.Domain.Core.Commands;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento
{
    public class IncluirTipoAtendimentoCommand:Command
    {
        public IncluirTipoAtendimentoCommand(string tipo, string descricao, bool prioritario)
        {
            Tipo = tipo;
            Descricao = descricao;
            Prioritario = prioritario;
        }

        public Guid Id { get;  set; }
        public string Tipo { get;  set; }
        public string Descricao { get;  set; }
        public bool Prioritario { get;  set; }

    }
}
