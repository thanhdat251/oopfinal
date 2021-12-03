using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.Entities
{
    class Product
    {
        private int productId;
        private string productName;
        private double productPrice;

        public int ProductId { get => productId; set => productId = value; }
        public string ProductName { get => productName; set => productName = value; }
        public double ProductPrice { get => productPrice; set => productPrice = value; }

        public Product()
        {

        }

        public Product(int productId, string productName, double productPrice)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
        }
    }
}
