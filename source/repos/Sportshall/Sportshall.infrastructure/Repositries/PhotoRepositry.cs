using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries
{
    public class PhotoRepositry :GenericRepositry<Photo>, IPhotoRepositry
    {
        public PhotoRepositry(AppDbContext Context) : base(Context)
        {
            
        }
    }
}
