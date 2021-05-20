using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Entities
{
    public enum ResultStatus
    {
        Error,
        Ok
    }

    public enum OrderStatus
    {
        CREATED, PAYED, REJECTED
    }
}
