using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NPL.SMS.R2S.Training.Entities;
using NPL.SMS.R2S.Training.DAO;
using NPL.SMS.Connection;
using System.Data;

namespace NPL.SMS.R2S.Training.DAO
{
    class CustomerDAO : ICustomerDAO
    {
        // SQL command
        private const string SELLECTCUSTOMER = "Select * From Customer";
        private const string UPDATE = "exec sp_updateCustomer @customer_id, @customer_name";
        private const string GETALLCUSTOMER = "SELECT DISTINCT Customer.Customer_id,customer_name FROM dbo.Orders,dbo.Customer WHERE Customer.Customer_id = dbo.Orders.Customer_id";
        private const string INSERT = "sp_addCustomer @name";

        /// <summary>
        /// Function 7: Update a customer in the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateCustomer(Customer customer)
        {
            if (CheckCustomerId(customer.CustomerId) == true)
            {
                using SqlConnection conn = Common.GetSqlConnection();

                conn.Open();

                using SqlCommand cmd = Common.GetSqlCommand(UPDATE, conn);
                
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@customer_id", customer.CustomerId),
                    new SqlParameter("@customer_name", customer.CustomerName),
                }
                );
               
                if (cmd.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine("Update successful.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Update failed.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("This customer id is not exit in database.");
                return false;
            }   
        }

        /// <summary>
        /// Check customer id
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool CheckCustomerId(int customerID)
        {
            using SqlConnection conn = Common.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Common.GetSqlCommand(SELLECTCUSTOMER, conn);

            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (customerID == (int)dataReader["customer_id"])
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Function 1: Select cusID,cusName have order
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public List<Customer> GetAllCustomer()
        {
            using SqlConnection conn = Common.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Common.GetSqlCommand(GETALLCUSTOMER, conn);
         
            using SqlDataReader dataReader = cmd.ExecuteReader();
            
            List<Customer> list = new List<Customer>();
            while (dataReader.Read())
            {
                Customer customer = new Customer
                {
                    CustomerId = dataReader.GetInt32(0),
                    CustomerName = dataReader.GetString(1)
                };
                
                list.Add(customer);
            }
            conn.Close();
            return list;
        }

        /// <summary>
        /// Function 5 Add a Customer
        /// </summary>
        public bool AddCustomer(Customer customer)
        {
            using SqlConnection conn = Common.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Common.GetSqlCommand("sp_addCustomer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", customer.customerName);

            if (cmd.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Function 6 Delete a Customer
        /// </summary>
        public bool DeleteCustomer(int customerID)
        {
            if(CheckCustomerId(customerID)== true)
            {
                using SqlConnection conn = Common.GetSqlConnection();

                conn.Open();

                using SqlCommand cmd = Common.GetSqlCommand("sp_deleteCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", customerID);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine("Delete success");
                    return true;
                }
                else
                {
                    return false;
                }
            }else
            {
                Console.WriteLine("CustomerId not exists on database");
                return false;
            }
            
        }
    } 
}
