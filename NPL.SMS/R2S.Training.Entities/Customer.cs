using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPL.SMS.R2S.Training.DAO;

namespace NPL.SMS.R2S.Training.Entities
{
    class Customer
    {
        public int customerId;
        public string customerName;

        public int CustomerId { get => customerId; set => customerId = value; }
        public string CustomerName { get => customerName; set => customerName = value; }

        public Customer()
        {

        }

        public Customer(int customerId, string customerName)
        {
            CustomerId = customerId;
            CustomerName = customerName;

        }
    }
}
