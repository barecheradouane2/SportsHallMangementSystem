using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class Revenues : BaseEntity<int>
    {

        

        public Revenues() { }
        public Revenues(RevenueType revenueType, int relatedId, decimal amount, DateTime revenueDate, string? note)
        {
            RevenueType = revenueType;
            RelatedId = relatedId;
            Amount = amount;
            RevenueDate = revenueDate;
            Note = note;
        }

        public RevenueType RevenueType { get; set; } = RevenueType.other;

        // ID from either the Subscription table or ProductSales table or can be other 
        public int? RelatedId { get; set; }

        public decimal Amount { get; set; }

        public DateTime RevenueDate { get; set; } = DateTime.Today;

        public string? Note { get; set; }








    }
}
