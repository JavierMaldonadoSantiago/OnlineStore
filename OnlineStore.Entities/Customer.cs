using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OnlineStore.Entities
{
    [Serializable]
    [DataContract]
    public class Customer
    {
        public Customer()
        {
            this.CustomerId = 0;
            this.CustomerName = string.Empty;
            this.CustomerEmail = string.Empty;
            this.CustomerMobil = string.Empty;
        }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerEmail { get; set; }
        [DataMember]
        public string CustomerMobil { get; set; }
    }
}
