using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.Core.Services;
using Sportshall.Core.Sharing;
using Sportshall.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sportshall.infrastructure.Repositries.Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public SubscriptionService(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }


        public async Task<SubscriptionsDTO> AddSubscriptionAsync(AddSubscriptionsDTO subscriptionDTO)
        {
            var offer= await _context.Offers.FirstOrDefaultAsync(m=>m.Id == subscriptionDTO.OffersID);

            var member= await _context.Members.FirstOrDefaultAsync(m=>m.Id==subscriptionDTO.MembersID);

            string membername = "";

            membername = member?.FullName ?? "client";


            bool  isfullpaid= offer.price <= subscriptionDTO.Amount ?true:false;

            DateTime EndDate= DateTime.Now.AddDays(offer.DurationDays);

            decimal reminnigamount = subscriptionDTO.Amount - offer.price ;




            Subscriptions subscriptions = new Subscriptions(EndDate, subscriptionDTO.Amount, reminnigamount, isfullpaid, subscriptionDTO.OffersID,  subscriptionDTO.MembersID);

           var result  =await _context.Subscriptions.AddAsync(subscriptions);
            await _context.SaveChangesAsync();

           

           


            Revenues revenues = new Revenues(RevenueType.subscription, subscriptions.Id, subscriptionDTO.Amount, DateTime.Now, $"Subscription for {membername} on {offer.Name}");

            await _context.Revenues.AddAsync(revenues);


            await _context.SaveChangesAsync();

            var subscriptionsDTO = _mapper.Map<SubscriptionsDTO>(subscriptions);




            return subscriptionsDTO;
        }


        public async Task<List<SubscriptionsDTO>> GetAllSubscription(SubscriptionsParams subscriptionsParams)
        {

            var query =  _context.Subscriptions.Include(x => x.Member).Include(x => x.Offer)
                 .ThenInclude(o => o.Activities)
                .AsNoTracking();

            //if (!string.IsNullOrWhiteSpace(subscriptionsParams.OfferName))
            //{
            //    query = query.Where(x => x.Offer.Name.ToLower() == subscriptionsParams.OfferName.ToLower());
            //}

            //if (!string.IsNullOrWhiteSpace(subscriptionsParams.MemberFullName))
            //{
            //    query = query.Where(x => x.Member.FullName.ToLower() == subscriptionsParams.MemberFullName.ToLower());



            //}


            if (!string.IsNullOrEmpty(subscriptionsParams.Search))
            {
                var searchWords = subscriptionsParams.Search.Split(" ");

                query = query.Where(x => searchWords.All(word => x.Offer.Name.ToLower().Contains(word.ToLower()) || x.Member.FullName.ToLower().Contains(word.ToLower()) ||
                 x.Offer.Activities.Name.ToLower().Contains(word.ToLower()) || x.Id.ToString().Contains(word)
                ));




            }




            if (!string.IsNullOrEmpty(subscriptionsParams.StartDate))
            {
                var searchWords = subscriptionsParams.StartDate.Split(" ");


                query = query.Where(x =>
                searchWords.Any(word =>
                  x.StartDate.ToString("yyyy-MM-dd").Contains(word)));

            }


            if (!string.IsNullOrEmpty(subscriptionsParams.EndDate))
            {
                if (DateTime.TryParse(subscriptionsParams.EndDate, out var parsedEndDate))
                {
                    query = query.Where(x => x.EndDate.Date == parsedEndDate.Date);
                }
            }


            //if (!string.IsNullOrEmpty(subscriptionsParams.EndDate))
            //{
            //    var searchWords = subscriptionsParams.StartDate.Split(" ");


            //    query = query.Where(x =>
            //    searchWords.Any(word =>
            //      x.EndDate.ToString("yyyy-MM-dd").Contains(word)));

            //}

         


            if (subscriptionsParams.PageNumber > 0 && subscriptionsParams.PageSize > 0)
            {
                query = query.Skip((subscriptionsParams.PageNumber - 1) * subscriptionsParams.PageSize)
                             .Take(subscriptionsParams.PageSize);
            }

            var result = _mapper.Map<List<SubscriptionsDTO>>(query);

            return result;


        }



        public async Task<int> CountAsync(SubscriptionsParams subscriptionsParams)
        {


            var query = _context.Subscriptions.Include(x => x.Member).Include(x => x.Offer)
                 .ThenInclude(o => o.Activities)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(subscriptionsParams.Search))
            {
                var searchWords = subscriptionsParams.Search.Split(" ");

                query = query.Where(x => searchWords.All(word => x.Offer.Name.ToLower().Contains(word.ToLower()) || x.Member.FullName.ToLower().Contains(word.ToLower()) ||
                 x.Offer.Activities.Name.ToLower().Contains(word.ToLower()) || x.Id.ToString().Contains(word)
                ));




            }





            return  query.Count();

        }





        public  async Task<List<SubscriptionsDTO>> GetAllSubscription()
        {
            var subscriptionlist=await _context.Subscriptions.Include(x=>x.Member).Include(x=>x.Offer).ToListAsync();

            var result= _mapper.Map<List<SubscriptionsDTO>>(subscriptionlist);


            return result;


        }

        public async Task<SubscriptionsDTO> GetSubscriptionAsync(int subscriptionId)
        {

            var subscription= await _context.Subscriptions.Include(x => x.Member).Include(x => x.Offer).FirstOrDefaultAsync(x=>x.Id== subscriptionId);

            var result = _mapper.Map<SubscriptionsDTO>(subscription);
            
            return result;
        }

        public async Task<SubscriptionsDTO> UpdateSubscriptionAsync(UpdateSubscriptionsDTO subscriptionsDTO)
        {
            // Find the existing subscription by ID
            var existingSubscription = await _context.Subscriptions.Include(x => x.Member).Include(x => x.Offer)
                .FirstOrDefaultAsync(x => x.Id == subscriptionsDTO.ID);

            existingSubscription.Offer = await _context.Offers.FirstOrDefaultAsync(x => x.Id == subscriptionsDTO.OffersID);

            existingSubscription.Member = await _context.Members.FirstOrDefaultAsync(x => x.Id == subscriptionsDTO.MembersID);

            existingSubscription.MembersID = subscriptionsDTO.MembersID;
            existingSubscription.OffersID = subscriptionsDTO.OffersID;



            existingSubscription.StartDate = subscriptionsDTO.StartDate;
            existingSubscription.EndDate = subscriptionsDTO.StartDate.AddDays(existingSubscription.Offer.DurationDays);
            existingSubscription.Amount = subscriptionsDTO.Amount;
    

            if (existingSubscription.Offer == null || existingSubscription.Member == null)
            {
                return null; // Offer or Member not found
            }

            existingSubscription.ReaminnigAmount = subscriptionsDTO.Amount - existingSubscription.Offer.price;
            existingSubscription.IsFullPaid = existingSubscription.ReaminnigAmount < 0 ? false : true;



            if (existingSubscription == null)
            {
                return null; 
            }

            
            _mapper.Map(subscriptionsDTO, existingSubscription);

            var revenues = await _context.Revenues
                .FirstOrDefaultAsync(x => x.RelatedId == existingSubscription.Id && x.RevenueType == RevenueType.subscription);

            if (revenues != null)
            {
                revenues.Amount = subscriptionsDTO.Amount;
                revenues.RevenueDate = DateTime.Now;


              



                revenues.Note = $"Updated Subscription for {existingSubscription.Member.FullName} on {existingSubscription.Offer.Name}";

                _context.Revenues.Update(revenues);


            }
           

          



            await _context.SaveChangesAsync();

            
            var result = _mapper.Map<SubscriptionsDTO>(existingSubscription);

            return result;
        }



        public async Task<SubscriptionsDTO> DeleteSubscriptionAsync(int subscriptionId)
        {
            var subscription = await _context.Subscriptions.Include(x => x.Member).Include(x => x.Offer).FirstOrDefaultAsync(x => x.Id == subscriptionId);

            if (subscription == null)
            {
                return null; 
            }

            var result = _mapper.Map<SubscriptionsDTO>(subscription);

            _context.Subscriptions.Remove(subscription);

            var revenues = await _context.Revenues
                .FirstOrDefaultAsync(x => x.RelatedId == subscription.Id && x.RevenueType == RevenueType.subscription);

            if (revenues != null)
            {
                _context.Revenues.Remove(revenues);

            }

            await _context.SaveChangesAsync();

            return result;
        }

        public  async Task<decimal> GetTotalSubscriptionAsync(FilterParams filterParams)
        {

            var query = _context.Subscriptions.AsNoTracking();

           


            DateTime now = DateTime.Now;

            if (filterParams.filter == "today")
            {
                query = query.Where(x => x.StartDate.Date == now.Date);
            }
            else if (filterParams.filter == "month")
            {
                query = query.Where(x => x.StartDate.Month == now.Month && x.StartDate.Year == now.Year);
            }
            else if (filterParams.filter == "year")
            {
                query = query.Where(x => x.StartDate.Year == now.Year);
            }

            return (decimal)await query.CountAsync();





        }



        public async Task<List<MonthlyStat>> GetSubscriptionstats()
        {
            var result = new List<MonthlyStat>();
            var now = DateTime.UtcNow;

            for (int i = 5; i >= 0; i--) 
            {
                var targetDate = new DateTime(now.Year, now.Month, 1).AddMonths(-i);
                var monthStart = targetDate;
                var monthEnd = monthStart.AddMonths(1).AddDays(-1);

                int count = await _context.Subscriptions
                    .AsNoTracking()
                    .Where(s => s.StartDate >= monthStart && s.StartDate <= monthEnd)
                    .CountAsync();

                result.Add(new MonthlyStat
                {
                    Month = targetDate.ToString("MMMM"),
                    Count = count
                });
            }

            return result;
        }

    }
}
