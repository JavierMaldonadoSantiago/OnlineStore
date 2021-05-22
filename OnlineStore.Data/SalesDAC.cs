using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using OnlineStore.Entities;
namespace OnlineStore.Data
{
    public class SalesDAC : DataAccessComponent
    {
        /// <summary>
        /// Registra una nueva orden de compra con su detalle
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static Result NewOrder(Order order)
        {
            Result result = new Result();
            try
            {
                using (var db = new DbContext(CONNECTION_NAME))
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;

                    var pCustomerId = new SqlParameter("@CustomerId", order.CustomerId);
                    var pStatus = new SqlParameter("@Status", OrderStatus.CREATED.ToString());
                    var pProductId = new SqlParameter("@ProductId", order.Detail.ProductId);
                    var pPieces = new SqlParameter("@Pieces", order.Detail.Pieces);

                    object[] parameters = new object[] { pCustomerId, pStatus, pProductId, pPieces };

                    var orderId = db.Database
                          .SqlQuery<int>("Usp_Sales_AddOrder @CustomerId, @Status, @ProductId, @Pieces", parameters).SingleOrDefault();
                    result.Status = ResultStatus.Ok;
                    result.Message = "Se agrego la orden de forma correcta";
                    result.ObjectResult = orderId;
                }
            }
            catch (Exception ex)
            {
                result.Status = ResultStatus.Error;
                result.Message = ex.Message;
            }
            return result;

        }

        public static Result GetOrders(int customerId)
        {
            Result result = new Result();
            List<OrderItem> orders = new List<OrderItem>();
            try
            {
                using (var db = new DbContext(CONNECTION_NAME))
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;

                    var pCustomerId = new SqlParameter("@CustomerId", customerId);

                    object[] parameters = new object[] { pCustomerId };

                    orders = db.Database
                          .SqlQuery<OrderItem>("Usp_Sales_GetOrders @CustomerId", parameters).ToList();
                    result.ObjectResult = orders;
                    result.Status = ResultStatus.Ok;
                    result.Message = "Las ordenes se obtuvieron correctamente";
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
