using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Servcom.SGA.Service.Api.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(3, ErrorMessage = "A sigla deve ter 3 caracteres")]
        [RegularExpression("^[A-Z]*$", ErrorMessage = "Somente caracteres alfabéticos são permitidos")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "O nome é requerido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Setor é requerido")]
        public string Setor { get; set; }

    }
}
