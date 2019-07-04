using Servcom.SGA.Domain.Core.Models;
using System;

namespace Servcom.SGA.Domain.Configuracao
{
    public class ConfiguracaoConteudo : Entity<ConfiguracaoConteudo>
    {
        public ConfiguracaoConteudo(Guid id, int tipo, string descricao, bool ativo, string conteudo)
        {
            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            Ativo = ativo;
            Conteudo = conteudo;
        }

        private ConfiguracaoConteudo() { }
        public int Tipo { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public string Conteudo { get; private set; }

        public override bool EhValido()
        {
            return true;
        }

        public static class ConfiguracaoConteudoFactory
        {
            public static ConfiguracaoConteudo ConfiguracaoConteudoLista(Guid id, int tipo, string descricao, bool ativo)
            {
                return new ConfiguracaoConteudo()
                {
                    Id = id,
                    Tipo = tipo,
                    Descricao = descricao,
                    Ativo = ativo

                };
            }
        }
    }
}