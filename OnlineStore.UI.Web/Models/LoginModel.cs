using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.UI.Web.Models
{
    public class LoginModel
    {
       
            [Required]
            [Display(Name = "Correo")]
            public string UserName { get; set; }
            [Required]
            [Display(Name = "Clave")]
            public string Password { get; set; }
       
    }
}