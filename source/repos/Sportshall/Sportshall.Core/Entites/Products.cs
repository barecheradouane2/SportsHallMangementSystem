using Sportshall.Core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class Products : BaseEntity<int>
    {

        public Products() { } 

        public Products(string name, decimal oldPrice, decimal newPrice, int baseQty , Unit unit, int stockQty, string note)
        {
            Name = name;
            OldPrice = oldPrice;
            NewPrice = newPrice;
            BaseQty = baseQty;
            this.unit = unit;
            StockQty = stockQty;
            Note = note;
        }

        public string Name { get; set; }

        public decimal OldPrice { get; set; }

        public decimal NewPrice { get; set; }

        public int  BaseQty { get; set; } = 1;

        public Unit unit { get; set; } = Unit.Piece;

        public int StockQty { get; set; } = 1;

        public string? Note { get; set; }

        public List<ProductPhoto> Photos { get; set; } = new List<ProductPhoto>();

    





    }
}
