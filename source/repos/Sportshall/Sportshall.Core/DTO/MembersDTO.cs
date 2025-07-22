using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record MembersDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        [JsonIgnore]

        public int Status { get; set; }

        public string StatusName => Status switch
        {
            0 => "Inactive",
            1 => "Active",
            _ => "Unknown"
        };
    }



    public record AddMembersDTO
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public int Status { get; set; } 
    }


    public record UpdateMembersDTO: AddMembersDTO
    {
        public int ID { get; set; }

       
    }






}
