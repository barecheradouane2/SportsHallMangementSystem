using Sportshall.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Services
{
    public interface IGenerateToken
    {
      public  Task<string> GetAndCreateToken(AppUser user);
    }
}
