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
    class EmployeeDAO
    {
        private const string SELLECTEMPLOYEE = "Select * From Employee";

        /// <summary>
        /// Check employee id
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool CheckEmployeeId(int employeeID)
        {
            using SqlConnection conn = Common.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Common.GetSqlCommand(SELLECTEMPLOYEE, conn);

            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (employeeID == (int)dataReader["employee_id"])
                    return true;
            }
            return false;
        }
    }
}
