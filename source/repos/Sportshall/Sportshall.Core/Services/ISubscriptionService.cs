using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Services
{
    public interface ISubscriptionService
    {
        Task<SubscriptionsDTO> AddSubscriptionAsync(AddSubscriptionsDTO subscriptionDTO);

        Task<SubscriptionsDTO> UpdateSubscriptionAsync(UpdateSubscriptionsDTO subscriptionsDTO);

        Task<SubscriptionsDTO> DeleteSubscriptionAsync(int subscriptionId);

        Task<SubscriptionsDTO> GetSubscriptionAsync(int subscriptionId);

        Task<List<SubscriptionsDTO>> GetAllSubscription();

        Task<List<SubscriptionsDTO>> GetAllSubscription(SubscriptionsParams subscriptionsParams);

         Task<int> CountAsync(SubscriptionsParams subscriptionsParams);

        Task<decimal> GetTotalSubscriptionAsync(FilterParams filterParams);

        Task<List<MonthlyStat>> GetSubscriptionstats();


    }
}
