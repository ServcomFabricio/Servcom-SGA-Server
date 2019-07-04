using Servcom.SGA.Domain.Core.Commands;
using System;

namespace Servcom.SGA.Domain.Configuracao.Commands
{
    public class EditarConfiguracaoGeralCommand:Command
    {
        public EditarConfiguracaoGeralCommand(Guid id, string tituloPainelAtendimento, string textoFixoPainelAtendimento, bool entradaVideo, bool conteudoConfigurado)
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
        public bool ConteudoConfigurado { get; set; }
    }
}
