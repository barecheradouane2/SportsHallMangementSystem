using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class Members : BaseEntity<int>
    {

        public string   FullName { get; set; }
        
        public string PhoneNumber { get; set; }

        public int Status { get; set; } = 1;

    }
}
