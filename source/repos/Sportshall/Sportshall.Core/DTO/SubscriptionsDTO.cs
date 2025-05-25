using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record SubscriptionsDTO
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Amount { get; set; }

        public decimal ReaminnigAmount { get; set; }

        public bool IsFullPaid { get; set; }

        public string Status => IsFullPaid ? "Paid" : "Not Yet fully Paid";

        public string OfferName { get; set; }

        public string MemberName { get; set; }

        // Formatted Dates
   
    }


    public record AddSubscriptionsDTO
    {
        public DateTime StartDate { get; set; } = DateTime.Now;

      

        public decimal Amount { get; set; }

     



        public int OffersID { get; set; }

        public int MembersID { get; set; }


    }


    public record UpdateSubscriptionsDTO : AddSubscriptionsDTO {

        public int ID { get; set; }
    
    
    }





}
