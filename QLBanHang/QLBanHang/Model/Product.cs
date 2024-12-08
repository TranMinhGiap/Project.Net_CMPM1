using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Model
{
    public class Product
    {
        // Các trường private
        private string productID;
        private string productName;
        private string categoryName;
        private string supplierName;
        private decimal productPrice;
        private int productQuantity;  

        // Constructor
        public Product() { }
        public Product(string productID, string productName, string categoryName, string supplierName, decimal productPrice, int productQuantity)
        {
            this.productID = productID;
            this.productName = productName;
            this.categoryName = categoryName;
            this.supplierName = supplierName;
            this.productPrice = productPrice;
            this.productQuantity = productQuantity;
        }

        // Các phương thức getter và setter

        public string ProductID { get { return productID; } set { productID = value; } }
        public string ProductName { get { return productName; } set { productName = value; } }
        public string CategoryName { get { return categoryName; } set { categoryName = value; } }
        public string SupplierName { get { return supplierName; } set { supplierName = value; } }
        public decimal ProductPrice { get { return productPrice; } set { productPrice = value; } }
        public int ProductQuantity { get { return productQuantity; } set { productQuantity = value; } }

        // Phương thức tính tổng giá (productPrice * productQuantity)
        public decimal GetTotalPrice()
        {
            return productPrice * productQuantity;
        }
    }
}
