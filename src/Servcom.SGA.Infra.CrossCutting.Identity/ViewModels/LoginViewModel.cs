using System.ComponentModel.DataAnnotations;

namespace Servcom.SGA.Infra.CrossCutting.Identity.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="A sigla é requerida")]
        public string Sigla { get; set; }

        [Required(ErrorMessage ="A senha é requerida")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
