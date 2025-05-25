using Microsoft.AspNetCore.Http;
using Sportshall.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{


    public record ProductsDTO
    {


        public int Id { get; set; }
        public string Name { get; set; }

        public decimal OldPrice { get; set; }

        public decimal NewPrice { get; set; }

        public int BaseQty { get; set; }

        [JsonIgnore]
        public Unit unit { get; set; }

        public string unitName { get; set; }


        public int StockQty { get; set; }

        public string Note { get; set; }

       public List<ProductsPhotoDTO> Photos { get; set; } = new();

    }

    //public virtual ICollection<PhotoDTO> Photos { get; set; }



    public record ProductsPhotoDTO
  (
      string ImageName,
      int ProductID
  );

    public record AddProductsDTO
    {
        public string Name { get; set; }

        public decimal OldPrice { get; set; }

        public decimal NewPrice { get; set; }

        public int BaseQty { get; set; } 

        public Unit unit { get; set; } 

        public int StockQty { get; set; }

        public string? Note { get; set; }

       
        public IFormFileCollection Photos { get; set; }

    }

    public record  UpdateProductsDTO : AddProductsDTO
    {
        public int Id { get; set; }

    }


    public record AddQtyToProduct {

        public int Id { get; set; }

        public decimal OldPrice { get; set; }

        public int StockQty { get; set; }

      

    }













}
