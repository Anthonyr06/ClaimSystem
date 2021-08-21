using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClaimSystem.Models
{
    public class Login
    {
        [Required, StringLength(254)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password), Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}