using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Entities;

namespace OnlineStore.UI.Web.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Clave")]
        public string RPassword { get; set; }

        public UserSession GetUserSession()
        {
            return new UserSession()
            {
                Name = this.Name,
                Email = this.Email,
                Mobile = this.Mobile,
                Password = this.RPassword,
                CustomerId = 0
            };
        }
    }
}