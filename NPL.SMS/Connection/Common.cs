using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace NPL.SMS.Connection
{
    class Common
    {
        // Connection string (chuỗi kết nối)
        private const string CONN_STRING = "Server=DESKTOP-08U6G7C\\SQLEXPRESS;Database=oop;Trusted_Connection=True;MultipleActiveResultSets=true";

        // Creat a connection
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection conn = new SqlConnection(CONN_STRING);
            return conn;
        }

        // Creat a SqlCommand
        public static SqlCommand GetSqlCommand(string query, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            return cmd;
        }
    }
}
