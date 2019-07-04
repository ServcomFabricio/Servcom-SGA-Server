using Servcom.SGA.Domain.Core.Events;
using System;

namespace Servcom.SGA.Domain.Configuracao.Events
{
    public class ConfiguracaoConteudoRegistradoEvent:Event
    {
        public ConfiguracaoConteudoRegistradoEvent(Guid id, int tipo, string descricao, bool ativo, string conteudo)
        {
            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            Ativo = ativo;
            Conteudo = conteudo;

            AggregateId = id;
        }

        public Guid Id { get; set; }
        public int Tipo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string Conteudo { get; set; }
    }
}
