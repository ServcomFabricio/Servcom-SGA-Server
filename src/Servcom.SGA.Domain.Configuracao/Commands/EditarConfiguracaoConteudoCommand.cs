using Servcom.SGA.Domain.Core.Commands;
using System;

namespace Servcom.SGA.Domain.Configuracao.Commands
{
    public class EditarConfiguracaoConteudoCommand:Command
    {
        public EditarConfiguracaoConteudoCommand(Guid id, int tipo, string descricao, bool ativo, string conteudo)
        {
            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            Ativo = ativo;
            Conteudo = conteudo;
        }

        public Guid Id { get; set; }
        public int Tipo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string Conteudo { get; set; }
    }
}
