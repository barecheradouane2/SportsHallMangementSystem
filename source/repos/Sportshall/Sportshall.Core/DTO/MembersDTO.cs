using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record MembersDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public int Status { get; set; } 
    }


    public record AddMembersDTO
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public int Status { get; set; } 
    }







}
