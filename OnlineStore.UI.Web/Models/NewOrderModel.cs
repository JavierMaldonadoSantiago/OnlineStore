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
        public int Pieces { get; set; }
        public string Producto { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
    }

    public class OrderModel
    {
        public OrderItem ActiveOrder { get; set; }
        public string Message { get; set; }
    }
}