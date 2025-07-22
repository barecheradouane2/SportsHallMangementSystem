using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.Core.Sharing;
using Sportshall.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries
{
    public class RevenuesRepositry : GenericRepositry<Revenues>, IRevenuesRepositry
    {
        private IMapper mapper;
        private readonly AppDbContext context;

        public RevenuesRepositry(AppDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
            this.context = context;
        }


        public async Task<IEnumerable<RevenuesDTO>> GetAllAsync(GeneralParams generalParams)
        {
            var query = context.Revenues.AsNoTracking();

            query = query.OrderByDescending(x => x.RevenueDate);

            if (generalParams.Sort == "asc")
            {
                query = query.OrderBy(x => x.RevenueDate);
            }
            

            if (!string.IsNullOrEmpty(generalParams.Search))
            {
                var searchWords = generalParams.Search
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var validDates = searchWords
                    .Where(word => DateTime.TryParse(word, out _))
                    .Select(word => DateTime.Parse(word).Date)
                    .ToList();

                var textWords = searchWords
                    .Where(word => !DateTime.TryParse(word, out _))
                    .Select(word => word.ToLower())
                    .ToList();

                
                query = query.Where(x =>
                    validDates.Count == 0 || validDates.Contains(x.RevenueDate.Date));
            }

          
            var revenues = await query.ToListAsync(); 

            if (!string.IsNullOrEmpty(generalParams.Search))
            {
                var searchWords = generalParams.Search
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var textWords = searchWords
                    .Where(word => !DateTime.TryParse(word, out _))
                    .Select(word => word.ToLower())
                    .ToList();

                revenues = revenues
                    .Where(x =>
                        textWords.All(word =>
                          x.Amount.ToString("0.##").Equals(word, StringComparison.OrdinalIgnoreCase) ||
                            x.RevenueType.ToString().ToLower().Contains(word)
                        )
                    ).ToList();
            }

            
            if (generalParams.PageNumber > 0 && generalParams.PageSize > 0)
            {
                revenues = revenues
                    .Skip((generalParams.PageNumber - 1) * generalParams.PageSize)
                    .Take(generalParams.PageSize)
                    .ToList();
            }

            var result = mapper.Map<List<RevenuesDTO>>(revenues);
            return result;
        }

        public  async Task<decimal> GetTotalRevenueAsync(FilterParams filterParams)
        {
            var query = context.Revenues.AsNoTracking();

            query = query.OrderByDescending(x => x.RevenueDate);

            DateTime now = DateTime.Now;

            if (filterParams.filter == "today")
            {
                query = query.Where(x => x.RevenueDate.Date == now.Date);
            }
            else if (filterParams.filter == "month")
            {
                query = query.Where(x => x.RevenueDate.Month == now.Month && x.RevenueDate.Year == now.Year);
            }
            else if (filterParams.filter == "year")
            {
                query = query.Where(x => x.RevenueDate.Year == now.Year);
            }

            decimal total = await query.SumAsync(x => (decimal?)x.Amount) ?? 0;



            return total;
        }
    }
}
