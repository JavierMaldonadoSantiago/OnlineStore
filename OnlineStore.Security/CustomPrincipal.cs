using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Security
{
    public class CustomPrincipal : IPrincipal
    {

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role) { return false; }

        public CustomPrincipal(CustomIdentity identity)
        {
            Identity = identity;
        }
    }
}
