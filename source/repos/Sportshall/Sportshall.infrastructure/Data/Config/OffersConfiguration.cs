using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportshall.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Data.Config
{
    public class OffersConfiguration : IEntityTypeConfiguration<Offers>
    {
        public void Configure(EntityTypeBuilder<Offers> builder)
        {
            builder.Property(o => o.Name).IsRequired().HasMaxLength(100);
          
            builder.Property(o => o.price).IsRequired();

            //builder.HasData(
            //    new Offers
            //    {
            //        Id = 1,
            //        Name = "One Month",
            //        duration_days = 30,
            //        price = 1500,
            //        ActivitiesID = 3
            //    }
            //);
        }
    
    }
}
