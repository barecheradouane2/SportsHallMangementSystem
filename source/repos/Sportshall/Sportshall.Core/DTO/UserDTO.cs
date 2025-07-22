using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record UserDTO
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public record UserDTODetails { 

        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }


    }


}
