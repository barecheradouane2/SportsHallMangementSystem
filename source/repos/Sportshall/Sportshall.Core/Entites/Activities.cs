using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public  class Activities : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }


        //each activities have list of photos
        public List<Photo> Photos { get; set; } = new List<Photo>();

        
    }
}
