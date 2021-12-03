using NPL.SMS.Connection;
using NPL.SMS.R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.DAO
{
    class LineItemDAO : ILineItemDAO
    {
        // SQL command
        private const string SELECTORDERID = "SELECT * FROM dbo.LineItem";
        private const string INSERT = "INSERT INTO dbo.LineItem(order_id,product_id,quantity) VALUES (@order_id, @product_id, @quantity);";

        /// <summary>
        /// Function 9 Add a LineItem
        /// </summary>
        public bool AddLineItem(LineItem lineItem)
        {
            if (OrderDAO.CheckOrderId(lineItem.OrderID) == true && ProductDao.CheckProductId(lineItem.ProdcutId) == true)
            {
                using SqlConnection conn = Common.GetSqlConnection();

                conn.Open();

                using SqlCommand cmd = Common.GetSqlCommand(INSERT, conn);

                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@order_id", lineItem.OrderID),
                    new SqlParameter("@product_id", lineItem.ProdcutId),
                    new SqlParameter("@quantity", lineItem.Quantity),
                });

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine("ADD LineItem successful.");
                    return true;
                }
                else
                {
                    Console.WriteLine("ADD lineItem failed.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Some thing wrong, please check orderId or productId again ");
                return false;
            }
        }

        /// <summary>
        /// Function 3 Get All Item by OrderID
        /// </summary>
        public List<LineItem> GetAllItemsByOderId(int orderId)
        {
            List<LineItem> list = new List<LineItem>();

            if (OrderDAO.CheckOrderId(orderId) == true)
            {
                using SqlConnection conn = Common.GetSqlConnection();

                conn.Open();

                using SqlCommand cmd = Common.GetSqlCommand(SELECTORDERID, conn);

                using SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    LineItem lineitem = new LineItem
                    {
                        OrderID = dataReader.GetInt32(0),
                        ProdcutId = dataReader.GetInt32(1),
                        Quantity = dataReader.GetInt32(2),
                        Price = dataReader.GetDouble(3),
                    };

                    if (orderId == lineitem.OrderID)
                        list.Add(lineitem);

                }

                conn.Close();
                return list;
            }
            else
            {
                Console.WriteLine("OrderID does not have any LineItem ");
                return list;
            }
        }
    }
}
