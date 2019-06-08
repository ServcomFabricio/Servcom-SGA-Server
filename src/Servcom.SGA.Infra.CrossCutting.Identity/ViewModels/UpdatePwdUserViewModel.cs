using System;
using System.ComponentModel.DataAnnotations;

namespace Servcom.SGA.Infra.CrossCutting.Identity.ViewModels
{
    public class UpdatePwdUserViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A senha Atual é requida")]
        [StringLength(100, ErrorMessage = "A Senha Atual deve ter pelo menos 3  caracteres", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }


        [Required(ErrorMessage = "A senha é requida")]
        [StringLength(100, ErrorMessage = "A Senha Atual deve ter pelo menos 3  caracteres", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string SenhaConfirmacao { get; set; }

    }
}
