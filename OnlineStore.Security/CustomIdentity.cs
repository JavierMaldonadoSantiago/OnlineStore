using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using OnlineStore.Entities;

namespace OnlineStore.Security
{
    public class CustomIdentity : IIdentity
    {

        public IIdentity Identity { get; set; }
        public UserSession User { get; set; }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }

        public CustomIdentity(IIdentity identity)
        {
            Identity = identity;
            var customMembershipUser = (CustomMembershipUser)Membership.GetUser(identity.Name);

            if (customMembershipUser != null)
            {
                User = customMembershipUser.User;
            }
        }
    }
}
