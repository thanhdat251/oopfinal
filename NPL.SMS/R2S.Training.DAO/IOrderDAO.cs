using NPL.SMS.R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.DAO
{
    interface IOrderDAO
    {
        List<Order> GetAllOrdersById(int Customer_Id); //2

        Double ComputeOrderTotal(int orderID); // 4

        bool AddOrder(Order order); // 8

        bool UpdateOrderTotal(int orderID); //10
    }
}
