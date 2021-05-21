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
    public class UserSession
    {
        public UserSession()
        {
            this.CustomerId = 0;
            this.Email = string.Empty;
            this.Name = string.Empty;
            this.Mobile = string.Empty;
        }
        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Mobile { get; set; }

        [DataMember]
        public string Password { get; set; }
    }


}
