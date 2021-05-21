using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineStore.Entities;

namespace OnlineStore.UI.Web.Models
{
    public class OrdersModel
    {
        public List<OrderItem> Orders { get; set; }
    }
}