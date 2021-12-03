using NPL.SMS.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.DAO
{
    class ProductDao
    {
        //Sql command
        private const string SELECT = "SELECT * FROM dbo.Product";

        //Check productId exists
        public static bool CheckProductId(int productID)
        {
            using SqlConnection conn = Common.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Common.GetSqlCommand(SELECT, conn);

            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (productID == (int)dataReader["product_id"])
                    return true;
            }
            return false;
        }
    }
}
