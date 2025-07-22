using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Sharing
{
    public class UserResponse
    {
        public UserResponse(UserDTO user, string token)
        {
            User = user;
            this.token = token;
        }

        public UserDTO User { get; set; }

        public string token { get; set; }


    }
}
