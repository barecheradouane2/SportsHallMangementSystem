using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record ActivitiesDTO
    (
           int ID ,
           string Name,
        string Description,

        List<PhotoDTO> Photos

      );

   


    public record PhotoDTO
    (
        string ImageName,
        int ActivitiesID
    );


    public record AddActivitiesDTO {

        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFileCollection Photos { get; set; }




    }

    public record class UpdateActivitiesDTO : AddActivitiesDTO
    {

        public int Id { get; set; }


    }








}
