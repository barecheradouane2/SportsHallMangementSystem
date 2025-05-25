using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Sharing
{
    public class ActivitesParams
    {


        public string? Sort { get; set; } 

        public int MaxPageSize { get; set; } = 9;

        public string? Search { get; set; } 

        private int _pageSize=5 ;


        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }


        public  int PageNumber { get; set; } = 1;








    }
}
