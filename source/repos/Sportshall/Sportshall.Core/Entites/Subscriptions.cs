using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class Subscriptions : BaseEntity<int>
    {
        public Subscriptions() { }
        public Subscriptions(DateTime endDate, decimal amount, decimal reaminnigAmount , bool IsFullPaid, int offersID, int membersID)
        {
            EndDate = endDate;
            this.IsFullPaid = IsFullPaid;
            Amount = amount;
            ReaminnigAmount = reaminnigAmount;

            OffersID = offersID;
          
            MembersID = membersID;
           
        }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }


        public decimal Amount { get; set; }


        public decimal ReaminnigAmount { get; set; }




        public bool IsFullPaid { get; set; } = true;




        [NotMapped]
        public string Status => IsFullPaid ? "Paid" : "Not Yet fully Paid";




        [ForeignKey(nameof(OffersID))]
        public int OffersID { get; set; }

        public virtual Offers Offer { get; set; }


        [ForeignKey(nameof(MembersID))]
        public int MembersID { get; set; }

        public virtual Members Member { get; set; }




    }
}
