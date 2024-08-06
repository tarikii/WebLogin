using Microsoft.Owin;
using Owin;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(WebLogin.Models.LoginViewModel))]

namespace WebLogin.Models
{
    public class LoginViewModel
    {
        // system.data annotations
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
