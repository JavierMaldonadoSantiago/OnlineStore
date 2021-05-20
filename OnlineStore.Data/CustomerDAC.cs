using OnlineStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace OnlineStore.Data
{
    public class CustomerDAC : DataAccessComponent
    {
        public static Result RegisterCustomer(Customer customer)
        {
            Result result = new Result();
            try
            {
                using (var db = new DbContext(CONNECTION_NAME))
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;

                    var pCustomerName = new SqlParameter("@CustomerName", customer.CustomerName);
                    var pCustomerEmail = new SqlParameter("@CustomerEmail", customer.CustomerEmail);
                    var pCustomerMobile = new SqlParameter("@CustomerMobile", customer.CustomerMobil);

                    object[] parameters = new object[] { pCustomerName, pCustomerEmail, pCustomerMobile };

                    var customerId = db.Database
                          .SqlQuery<int>("Usp_Cust_AddCustomer @CustomerName, @CustomerEmail, @CustomerMobile", parameters).SingleOrDefault();

                }
            }
            catch (Exception ex)
            {
                result.Status = ResultStatus.Error;
                result.DetailError = ex.Message;
            }
            return result;
        }
        public  static int IsCustomerRegistered(Customer customer)
        {
            int customerId;
            try
            {
                using (var db = new DbContext(CONNECTION_NAME))
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;

                    var pCustomerName = new SqlParameter("@CustomerName", customer.CustomerName);
                    var pCustomerEmail = new SqlParameter("@CustomerEmail", customer.CustomerEmail);
                    var pCustomerMobile = new SqlParameter("@CustomerMobile", customer.CustomerMobil);

                    object[] parameters = new object[] { pCustomerName, pCustomerEmail, pCustomerMobile };

                    customerId = db.Database
                          .SqlQuery<int>("Usp_Cust_GetCustomerId @CustomerName, @CustomerEmail, @CustomerMobile", parameters).SingleOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerId;
        }
    }
    
}
