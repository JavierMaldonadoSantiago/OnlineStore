using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Product { get; set; }
        public int Pieces { get; set; }
        public decimal Amount { get; set; }

        public string Status { get; set; }

        public string StatusDesc
        {
            get
            {
                string status = string.Empty;
                switch (Status)
                {
                    case "CREATED":
                        status = "Pendiente de pago";
                        break;
                    case "PAYED":
                        status = "Pagada";
                        break;
                    case "REJECTED":
                        status = "Pago Rechazado";
                        break;
                    default:
                        status = string.Empty;
                        break;
                }
                return status;

            }
           
        }
    }
}
