using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Entities;

namespace OnlineStore.UI.Web.Models
{
    public class NewOrderModel
    {
        [Required]
        [Display(Name = "Piezas")]
        public int Pieces { get; set; }
        public string Producto { get; set; }
    }
}