using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Entities
{
    public class Result
    {
        public ResultStatus Status { get; set; }
        public object ObjectResult { get; set; }
        public string DetailError { get; set; }
    }
}
