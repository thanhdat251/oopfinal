using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.Entities
{
    class Order
    {
        private int orderId;
        private DateTime orderDate;
        private int customerID;
        private int employeeID;
        private double total;
        
        public int OrderId { get => orderId; set => orderId = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public int CustomerID { get => customerID; set => customerID = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public double Total { get => total; set => total = value; }

        public Order()
        {

        }

        public Order(int orderId, DateTime orderDate, int customerID, int employeeID, double total)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            CustomerID = customerID;
            EmployeeID = employeeID;
            Total = total;
        }
    }
}
