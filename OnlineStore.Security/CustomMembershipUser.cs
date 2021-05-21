using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using OnlineStore.Entities;

namespace OnlineStore.Security
{
    public class CustomMembershipUser : MembershipUser
    {
        public UserSession User { get; set; }

        public CustomMembershipUser(UserSession user)
            : base("CustomMembershipProvider", user.Name, user.CustomerId, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            User = user;
        }

    }
}
