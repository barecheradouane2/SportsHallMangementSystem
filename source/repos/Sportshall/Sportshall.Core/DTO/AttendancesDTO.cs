using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;







namespace Sportshall.Core.DTO
{
    public record AttendancesDTO
    {

        public int ID { get; set; }


        [Column(TypeName = "date")]

         [JsonConverter(typeof(DateOnlyJsonConverter))]

        public DateTime AttendanceDate { get; set; }

        public int Status { get; set; }

        public string StatusName => Status == 1 ? "Present" : "Absent";

        public string MemberFullName { get; set; }

        public string ActivityName { get; set; }


    }

    public record AddAttendancesDTO
    {
        public DateTime AttendanceDate { get; set; } 

        public int Status { get; set; } 

        public int MembersID { get; set; }

        public int ActivitiesID { get; set; }


    }


    public record UpdateAttendancesDTO : AddAttendancesDTO
    {
        public int ID { get; set; }

    }







    }
