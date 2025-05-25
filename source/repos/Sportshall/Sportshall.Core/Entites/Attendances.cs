using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Sportshall.Core.Entites
{
    public class Attendances : BaseEntity<int>
    {


        [Column(TypeName = "date")]
        public DateTime AttendanceDate { get; set; } = DateTime.Now;


        public int Status { get; set; } = 1;


        [ForeignKey(nameof(MembersID))]
        public int MembersID { get; set; }

        public virtual Members Member { get; set; }


        [ForeignKey(nameof(ActivitiesID))]
        public int ActivitiesID { get; set; }

        public virtual Activities Activities { get; set; }




    }
}
