using Sportshall.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record RevenuesDTO
    {
        public int ID { get; set; } 

        public RevenueType RevenueType { get; set; }

        public string RevenueName => RevenueType.ToString();


        public int? RelatedId { get; set; }

        public decimal Amount { get; set; }

        public DateTime RevenueDate { get; set; } 

        public string? Note { get; set; }

    }


    public record AddRevenuesDTO
    {
        //public RevenueType RevenueType { get; set; }

        //public int? RelatedId { get; set; }

        public decimal Amount { get; set; }

        public DateTime RevenueDate { get; set; }

        public string? Note { get; set; }

    }

    public record UpdateRevenuesDTO : AddRevenuesDTO
    {
        public int ID { get; set; }

    }
 }
