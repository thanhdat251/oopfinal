using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPL.SMS.R2S.Training.Entities;

namespace NPL.SMS.R2S.Training.DAO
{
    interface ILineItemDAO
    {
        List<LineItem> GetAllItemsByOderId(int orderId); //3

        bool AddLineItem(LineItem lineItem); //9


    }
}
