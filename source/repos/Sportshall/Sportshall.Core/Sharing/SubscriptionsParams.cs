using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Sharing
{
    public class SubscriptionsParams : GeneralParams
    {

        public string? MemberFullName { get; set; }

        public string? OfferName { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }

    }
}
