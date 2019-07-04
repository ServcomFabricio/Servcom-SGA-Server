using Servcom.SGA.Domain.Core.Models;
using System;

namespace Servcom.SGA.Domain.Configuracao
{
    public class ConfiguracaoGeral : Entity<ConfiguracaoGeral>
    {
        public ConfiguracaoGeral(Guid id,string tituloPainelAtendimento, string textoFixoPainelAtendimento,bool entradaVideo, bool conteudoConfigurado)
        {
            Id = id;
            TituloPainelAtendimento = tituloPainelAtendimento;
            TextoFixoPainelAtendimento = textoFixoPainelAtendimento;
            EntradaVideo = entradaVideo;
            ConteudoConfigurado = conteudoConfigurado;
        }

        public string TituloPainelAtendimento { get; private set; }
        public string TextoFixoPainelAtendimento { get;private set; }
        public bool EntradaVideo { get;private set; }
        public bool ConteudoConfigurado { get;private set; }

        public override bool EhValido()
        {
            return true;
        }
    }
}
