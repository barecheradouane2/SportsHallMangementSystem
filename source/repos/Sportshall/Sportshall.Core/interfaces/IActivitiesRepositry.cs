using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.interfaces
{
    public interface IActivitiesRepositry : IGenericRepositry<Activities>
    {
        //for futur  methods
        // Add any additional methods specific to Activities here
        // For example:
        // Task<IEnumerable<Activities>> GetActivitiesByCategoryAsync(string category);

        Task<IEnumerable<ActivitiesDTO>> GetAllAsync(ActivitesParams activitesParams);

        Task<bool> AddAsync(AddActivitiesDTO activitiesDTO);

        Task<bool> UpdateAsync(UpdateActivitiesDTO activitiesDTO);

        Task<bool> DeleteAsync(Activities activities);
    } 
    
}
