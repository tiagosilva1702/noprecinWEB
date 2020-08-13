using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.Models
{
    public class Usuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }

    public class UsuarioAutenticado
    {
        public Guid id { get; set; }
        public string email { get; set; }
        public string acessToken { get; set; }
    }


    public enum Perfil
    {
        administrador,
        usuario
    }
}
