using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class AppUser :IdentityUser
    {

        public string DisplayName { get; set; } = string.Empty; 
    }
}
