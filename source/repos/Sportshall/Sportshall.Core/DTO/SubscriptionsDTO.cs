using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record SubscriptionsDTO
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime EndDate { get; set; }

        public decimal Amount { get; set; }

        public decimal ReaminnigAmount { get; set; }

        public bool IsFullPaid { get; set; }

        public string Status => IsFullPaid ? "Paid" : "Not  Paid";

        public string OfferName { get; set; }

        public string MemberName { get; set; }

        public string ActivitiesName { get; set; }

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
