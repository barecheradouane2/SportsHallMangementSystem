using Sportshall.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.interfaces
{
    public interface IPhotoRepositry : IGenericRepositry<Photo>
    {
        // Add any additional methods specific to Photo repository here
        // For example:
        // Task<IEnumerable<Photo>> GetPhotosByActivityIdAsync(int activityId);
    }
   
}
