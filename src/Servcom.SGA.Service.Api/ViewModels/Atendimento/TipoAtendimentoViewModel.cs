using System;
using System.ComponentModel.DataAnnotations;

namespace Servcom.SGA.Service.Api.ViewModels.Atendimento
{
    public class TipoAtendimentoViewModel
    {
        public TipoAtendimentoViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [StringLength(3, ErrorMessage = "O tipo deve ter 3 caracteres")]
        [RegularExpression("^[A-Z]*$", ErrorMessage = "Somente caracteres alfabéticos são permitidos")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "A descrição é requerida")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Iforme se o tipo será")]
        public bool Prioritario { get; set; }
    }
}
