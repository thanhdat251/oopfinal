using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.Entities
{
    class LineItem
    {
        private int orderID;
        private int prodcutId;
        private int quantity;
        private double price;

        public int OrderID { get => orderID; set => orderID = value; }
        public int ProdcutId { get => prodcutId; set => prodcutId = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }

        public LineItem()
        {
           
        }

        public LineItem(int orderID, int prodcutId, int quantity, double price)
        {
            OrderID = orderID;
            ProdcutId = prodcutId;
            Quantity = quantity;
            Price = price;
        }
    }   

}
