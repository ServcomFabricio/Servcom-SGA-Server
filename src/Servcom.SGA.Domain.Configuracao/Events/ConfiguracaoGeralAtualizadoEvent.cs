using Servcom.SGA.Domain.Core.Events;
using System;

namespace Servcom.SGA.Domain.Configuracao.Events
{
    public class ConfiguracaoGeralAtualizadoEvent:Event
    {
        public ConfiguracaoGeralAtualizadoEvent(Guid id, string tituloPainelAtendimento, string textoFixoPainelAtendimento, bool entradaVideo, bool conteudoConfigurado)
        {
            Id = id;
            TituloPainelAtendimento = tituloPainelAtendimento;
            TextoFixoPainelAtendimento = textoFixoPainelAtendimento;
            EntradaVideo = entradaVideo;
            ConteudoConfigurado = conteudoConfigurado;
        }

        public Guid Id { get; set; }
        public string TituloPainelAtendimento { get; set; }
        public string TextoFixoPainelAtendimento { get; set; }
        public bool EntradaVideo { get;  set; }
        public bool ConteudoConfigurado { get;  set; }
    }
}
