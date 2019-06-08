using System;
using System.ComponentModel.DataAnnotations;

namespace Servcom.SGA.Infra.CrossCutting.Identity.ViewModels
{
    public class UpdateUserViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A sigla é requerida")]
        [StringLength(3,ErrorMessage ="A sigla deve ter 3 caracteres")]
        [RegularExpression("^[A-Z]*$", ErrorMessage = "Somente caracteres alfabéticos maúsculos são permitidos")]
        public string Sigla { get; set; }


        [Required(ErrorMessage = "O Nome é requerido")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O Setor é requerido")]
        public string Setor { get; set; }

    }
}
