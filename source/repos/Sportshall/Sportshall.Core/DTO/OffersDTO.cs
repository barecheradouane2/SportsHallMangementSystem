using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record OffersDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public int DurationDays { get; set; }

        public decimal price { get; set; }



        



        public string ActivitiesName { get; set; }


    }


    public record AddOffersDTO {

        public string Name { get; set; }

        public int DurationDays { get; set; }

        public decimal price { get; set; }

        public int ActivitiesID { get; set; }





    }






}
