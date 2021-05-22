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
        private string status = string.Empty;
        public string Status
        {
            get
            {
                switch (status)
                {
                    case "CREATED":
                        status = "Pendiente";
                        break;
                    case "PAYED":
                        status = "Pagada";
                        break;
                    case "REJECTED":
                        status = "Rechadado";
                        break;
                    default:
                        status = string.Empty;
                        break;
                }
                return status;

            }
            set
            {
                this.status = value;
            }
        }
    }
}
