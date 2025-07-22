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
    public interface IRevenuesRepositry  : IGenericRepositry<Revenues>
    {
        Task<IEnumerable<RevenuesDTO>> GetAllAsync(GeneralParams generalParams);

        Task<decimal> GetTotalRevenueAsync(FilterParams filterParams);
    }
}
