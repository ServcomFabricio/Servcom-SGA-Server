using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Servcom.SGA.Infra.CrossCutting.Identity.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "O nome é requerido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A sigla do usuário é requerida")]
        [StringLength(3)]
        public string Sigla { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} deve ter pelo menos {2} e no máximo {1} .", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação senha não correspondem.")]
        public string SenhaConfirmacao { get; set; }
    }
}
