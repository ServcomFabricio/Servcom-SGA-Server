using System;
using System.ComponentModel.DataAnnotations;

namespace Servcom.SGA.Service.Api.ViewModels.Configuracao
{
    public class ConfiguracaoConteudoViewModel
    {
        public ConfiguracaoConteudoViewModel(int tipo, string descricao, bool ativo, string conteudo)
        {
            Id = Guid.NewGuid();
            Tipo = tipo;
            Descricao = descricao;
            Ativo = ativo;
            Conteudo = conteudo;
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Informe o tipo do conteudo configurado")]
        public int Tipo { get; set; }

        [Required(ErrorMessage = "Informe a descrição do conteudo configurado")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o status do conteudo configurado")]
        public bool Ativo { get; set; }

        //[MinLength(3, ErrorMessage = "Informe  conteudo configurado")]
        [Required(ErrorMessage = "Informe o conteudo configurado")]
        public string Conteudo { get; set; }
    }
}
