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
    public class AttendancesConfiguration : IEntityTypeConfiguration<Attendances>
    {
        public void Configure(EntityTypeBuilder<Attendances> builder)
        {
           

        }
    }
    
}
