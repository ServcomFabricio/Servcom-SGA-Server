using System.ComponentModel.DataAnnotations;

namespace Servcom.SGA.Infra.CrossCutting.Identity.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "A sigla é requerida")]
        [StringLength(3,ErrorMessage ="A sigla deve ter 3 caracteres")]
        [RegularExpression("^[A-Z]*$", ErrorMessage = "Somente caracteres alfabéticos maúsculos são permitidos")]
        public string Sigla { get; set; }


        [Required(ErrorMessage = "O Nome é requerido")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O Setor é requerido")]
        public string Setor { get; set; }

        [Required(ErrorMessage ="A senha é requida")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string SenhaConfirmacao { get; set; }
    }
}
