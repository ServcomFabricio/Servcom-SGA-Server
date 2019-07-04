using System;
using System.ComponentModel.DataAnnotations;

namespace Servcom.SGA.Service.Api.ViewModels.Configuracao
{
    public class ConfiguracaoGeralViewModel
    {
        public ConfiguracaoGeralViewModel()
        {
            Id = Guid.NewGuid();   
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O titulo do Painel é Requerido")]
        public string TituloPainelAtendimento { get;  set; }

        [Required(ErrorMessage = "O texto fixo do Painel é Requerido")]
        public string TextoFixoPainelAtendimento { get;  set; }
        public bool EntradaVideo { get;  set; }
        public bool ConteudoConfigurado { get;  set; }
    }
}
