using NPL.SMS.R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.DAO
{
    interface ICustomerDAO
    {
        List<Customer> GetAllCustomer(); //1

        bool AddCustomer(Customer customer); //5

        bool DeleteCustomer(int customerID); //6

        bool UpdateCustomer(Customer customer); //7
        
    }
}
