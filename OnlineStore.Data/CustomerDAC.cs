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

                    if (customerId != 0)
                    {
                        customer.CustomerId = customerId;
                        result.ObjectResult = customer;
                        result.Status = ResultStatus.Ok;
                        result.Message = "Se registro el cliente correctamente";
                    }
                    else
                    {
                        result.Status = ResultStatus.Error;
                        result.Message = "No fue posible registrar el cliente";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                result.Status = ResultStatus.Error;
                result.Message = ex.Message;
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

                    var pCustomerEmail = new SqlParameter("@CustomerEmail", customer.CustomerEmail);

                    object[] parameters = new object[] { pCustomerEmail };

                    customerId = db.Database
                          .SqlQuery<int>("Usp_Cust_GetCustomerId @CustomerEmail", parameters).SingleOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerId;
        }

        public static Result GetCustomerByEmail(string email)
        {
            Result result = new Result();
            try
            {
                using (var db = new DbContext(CONNECTION_NAME))
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;

                    var pCustomerEmail = new SqlParameter("@CustomerEmail", email);

                    object[] parameters = new object[] { pCustomerEmail };

                    UserSession customer = db.Database
                          .SqlQuery<UserSession>("Usp_Cust_GetCustomerByName @CustomerEmail", parameters).SingleOrDefault();
                    if (customer != null)
                    {
                        result.Status = ResultStatus.Ok;
                        result.ObjectResult = customer;
                    }
                    else
                    {
                        result.Status = ResultStatus.Error;
                        result.Message = "Usuario no encontrado";
                    }
                    

                }
            }
            catch (Exception ex)
            {
                result.Status = ResultStatus.Error;
                result.Message = ex.Message;
            }
            return result;

        }
    }
    
}
