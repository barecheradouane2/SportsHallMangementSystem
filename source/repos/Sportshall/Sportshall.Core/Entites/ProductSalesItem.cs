using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class ProductSalesItem : BaseEntity<int>
    {
        public ProductSalesItem(int productsID, int qty, string productName, decimal totalPrice)
        {
            ProductsID = productsID;
            this.qty = qty;
            TotalPrice = totalPrice;
            ProductName = productName;
        }

        [ForeignKey(nameof(ProductSalesID))]
        public int ProductSalesID { get; set; }



        [ForeignKey(nameof(ProductsID))]
        public int ProductsID { get; set; }

       


        public string ProductName { get; set; }

       


        public int qty { get; set; } = 1;


        public decimal TotalPrice { get; set; } = 0;
    }
}
