using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportshall.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Data.Config
{
    public class ActivitiesConfiguration : IEntityTypeConfiguration<Activities>
    {
        public void Configure(EntityTypeBuilder<Activities> builder)
        {
           
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(500);

          

            //builder.HasData(
            //    new Activities
            //    {
            //        Id = 1,
            //        Name = "Gym",
            //        Description = " a principle activities in the hall"
            //    }

            //   );

        }
        //seeding data
    }
   
}
