using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class Offers : BaseEntity<int>
    {
        public string Name { get; set; }

        public int DurationDays { get; set; }

        public decimal price { get; set; }

       

        

        [ForeignKey(nameof(ActivitiesID))]
        public int ActivitiesID { get; set; }

        public virtual Activities Activities { get; set; }







    }
}
