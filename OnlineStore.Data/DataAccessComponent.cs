using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace OnlineStore.Data
{
    public class DataAccessComponent
    {
        protected const string CONNECTION_NAME = "DigitalizacionEntities";


        protected StringBuilder log = new StringBuilder();
        protected string ConnectionString
        {
            get
            {

                return ConfigurationManager.ConnectionStrings[CONNECTION_NAME].ConnectionString;
            }
        }
    }
}
