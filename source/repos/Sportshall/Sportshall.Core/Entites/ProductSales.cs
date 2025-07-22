using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class ProductSales : BaseEntity<int>
    {
        public ProductSales() { }
        public ProductSales(decimal totalPrice, decimal clientPayement, bool isFullPaid, int membersID)
        {
            TotalPrice = totalPrice;
            ClientPayement = clientPayement;
            IsFullPaid = isFullPaid;
            MembersID = membersID;
            ProductSalesItems = new List<ProductSalesItem>(); // initialize as needed
        }


        //public ProductSales( decimal totalPrice, decimal reaminnigPrice, bool isFullPaid, )
        //{

        //    TotalPrice = totalPrice;
        //    ReaminnigPrice = reaminnigPrice;
        //    IsFullPaid = isFullPaid;
        //}


        public decimal TotalPrice { get; set; } = 0;

        public decimal ClientPayement { get; set; } = 0;

        public bool IsFullPaid { get; set; } = true;


        [NotMapped]
        public string Status => IsFullPaid ? "Paid" : "Not Yet fully Paid";

        public DateTime SaleDate { get; set; } = DateTime.Today;

      


        public List<ProductSalesItem> ProductSalesItems { get; set; } =  new();


        [ForeignKey(nameof(MembersID))]
        public int? MembersID { get; set; }

        public virtual Members? Members { get; set; }




    }
}
