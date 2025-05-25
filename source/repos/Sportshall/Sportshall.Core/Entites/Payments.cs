using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class Payments : BaseEntity<int>
    {

     


        public DateTime PaymentDate { get; set; }

        public int Amount { get; set; }
       
        public string notes { get; set; }


        [ForeignKey(nameof(SubscriptionsID))]
        public int SubscriptionsID { get; set; }

        public virtual Subscriptions Subscription { get; set; }
    }
    
}
