using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NPL.SMS.R2S.Training.Entities;
using NPL.SMS.R2S.Training.DAO;
using NPL.SMS.Connection;

namespace NPL.SMS.R2S.Training.DAO
{
    class OrderDAO : IOrderDAO
    {
        // SQL command
        private const string SELLECTORDER = "SELECT * FROM dbo.Orders";
        private const string INSERT = @"insert into Orders (order_date, customer_id, employee_id, total)
                                         Values (@order_date, @customer_id, @employee_id, @total)";
        private const string SELECTORDERTOTAL = "Select dbo.FNC(@order_id)";
        private const string UPDATETOTAL = "UPDATE dbo.Orders SET total = @value WHERE order_id = @orderid";


        /// <summary>
        /// Function 8. Creat an order into the database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddOrder(Order order)
        {
            if (EmployeeDAO.CheckEmployeeId(order.EmployeeID) == true
                && CustomerDAO.CheckCustomerId(order.CustomerID) == true)
            {
                using SqlConnection conn = Common.GetSqlConnection();

                conn.Open();

                using SqlCommand cmd = Common.GetSqlCommand(INSERT, conn);

                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@order_date", order.OrderDate),
                    new SqlParameter("@customer_id", order.CustomerID),
                    new SqlParameter("@employee_id", order.EmployeeID),
                    new SqlParameter("@total", order.Total),
                }
                );

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine("ADD ORDER successful.");
                    return true;
                }
                else
                {
                    Console.WriteLine("ADD ORDER failed.");
                    return false;
                }
            }
            else
            {
                if (EmployeeDAO.CheckEmployeeId(order.EmployeeID) == false 
                    && CustomerDAO.CheckCustomerId(order.CustomerID) == true)
                {
                    Console.WriteLine("This employee id is not exit in database.");
                }
                else if (EmployeeDAO.CheckEmployeeId(order.EmployeeID) == true 
                    && CustomerDAO.CheckCustomerId(order.CustomerID) == false)
                {
                    Console.WriteLine("This customer id is not exit in database.");
                }
                else
                {
                    Console.WriteLine("This customer id or this employee id do not exit in database.");
                }      
                return false;
            }
        }

        /// <summary>
        /// Function 4 Compute total
        /// </summary>
        public double ComputeOrderTotal(int orderID)
        {
            if(CheckOrderId(orderID)== true)
            {
                using SqlConnection conn = Common.GetSqlConnection();
               
                conn.Open();
               
                using SqlCommand cmd = Common.GetSqlCommand(SELECTORDERTOTAL, conn);
                cmd.Parameters.AddRange(new[]
                {
                new SqlParameter("@order_id",orderID)
            });
                try
                {
                    return (double)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return 0;
            }
            else
            {
                Console.WriteLine("Order not exist on database");
                return 0;
            }
            
        }

        /// <summary>
        /// Function 2 Get all Order by CustomerId
        /// </summary>
        public List<Order> GetAllOrdersById(int Customer_Id)
        {
            List<Order> list = new List<Order>();
            if (CheckCustomerhasOrder(Customer_Id) == true)
            {
                using SqlConnection conn = Common.GetSqlConnection();

                conn.Open();

                using SqlCommand cmd = Common.GetSqlCommand(SELLECTORDER, conn);

                using SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Order order = new Order
                    {
                        OrderId = dataReader.GetInt32(0),
                        OrderDate = dataReader.GetDateTime(1),
                        CustomerID = dataReader.GetInt32(2),
                        EmployeeID = dataReader.GetInt32(3),
                        Total = dataReader.GetDouble(4)
                    };
                    if (Customer_Id == order.CustomerID)
                        list.Add(order);
                }
                conn.Close();
                return list;
            }
            else
            {
                Console.WriteLine("orderid not ex");
                return list;
            }
        }

        /// <summary>
        /// Function 10 Compute Order total
        /// </summary>
        public bool UpdateOrderTotal(int orderID)
        {
            using SqlConnection conn = Common.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Common.GetSqlCommand(UPDATETOTAL, conn);

            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@value", ComputeOrderTotal(orderID)),
                new SqlParameter("@orderid",orderID)
            });

            cmd.ExecuteNonQuery();
            return true;
        }

        //Check OrderId Exists
        public static bool CheckOrderId(int orderID)
        {
            using SqlConnection conn = Common.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Common.GetSqlCommand(SELLECTORDER, conn);

            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (orderID == (int)dataReader["order_id"])
                    return true;
            }
            return false;
        }

        //Check Customer has Order
        public static bool CheckCustomerhasOrder(int cusID)
        {
            using SqlConnection conn = Common.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Common.GetSqlCommand(SELLECTORDER, conn);

            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (cusID == (int)dataReader["customer_id"])
                    return true;
            }
            return false;
        }

    } 
}
