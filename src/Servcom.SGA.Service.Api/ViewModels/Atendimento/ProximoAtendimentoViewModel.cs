using System;
using System.ComponentModel.DataAnnotations;

namespace Servcom.SGA.Service.Api.ViewModels.Atendimento
{
    public class ProximoAtendimentoViewModel
    {
        [Required(ErrorMessage = "O Tipo é requerido")]
        [StringLength(3, ErrorMessage = "O tipo deve ter 3 caracteres")]
        [RegularExpression("^[A-Z]*$", ErrorMessage = "Somente caracteres alfabéticos são permitidos")]
        public string Tipo { get; set; }

        [DataType(DataType.Date,ErrorMessage ="Data não é valida")]
        [Required(ErrorMessage = "A data de Criação requerida")]
        public DateTime DataCriacao { get; set; }

        [StringLength(3, ErrorMessage = "A sigla do usuário deve ter 3 caracteres")]
        [RegularExpression("^[A-Z]*$", ErrorMessage = "Somente caracteres alfabéticos são permitidos")]
        public string Usuario { get; set; }
        
        [Required(ErrorMessage = "O Guiche é requerido")]
        public string Guiche { get; set; }

        public bool? Prioritario { get; set; }
    }
}
