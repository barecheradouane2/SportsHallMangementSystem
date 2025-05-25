using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record ProductSalesDTO
    {

        public int ID { get; set; }

   

        public decimal TotalPrice { get; set; } 

        public decimal ClientPayement { get; set; } 

        public bool IsFullPaid { get; set; }

        public string Status => IsFullPaid ? "Paid" : "Not Yet fully Paid";

        public DateTime SaleDate { get; set; }

        public int? MembersID { get; set; } = null;
        public string MemberName { get; set; }

        public List<ProductSalesItemDTO> ProductSalesItems { get; set; } = new List<ProductSalesItemDTO>();



    }

    public record AddProductSalesDTO
    {
        

    

        public decimal ClientPayement { get; set; } 

        public bool IsFullPaid { get; set; } 

        public DateTime SaleDate { get; set; } 

       

        public int MembersID { get; set; }

      public  List<AddProductSalesItemDTO> ProductSalesItems { get; set; } = new List<AddProductSalesItemDTO>();

    }

    public record UpdateProductSalesDTO : AddProductSalesDTO
    {
        public int ID { get; set; }

       

    }


    public record class ProductSalesItemDTO
    {
        public int ID { get; set; }

        public int ProductsID { get; set; }

        
        public string ProductName { get; set; } = string.Empty;

        public int qty { get; set; } = 1;

        public decimal TotalPrice { get; set; } = 0;



    }

    public record class AddProductSalesItemDTO
    {
        public int ProductsID { get; set; }

        public int qty { get; set; } = 1;

     

      

    }

    public record class UpdateProductSalesItemDTO : AddProductSalesItemDTO
    {
        public int ID { get; set; }

    }


    }
