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
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries
{
    public class ExpensesRepositry : GenericRepositry<Expenses>, IExpensesRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public ExpensesRepositry(AppDbContext Context, IMapper mapper) : base(Context)
        {
            context = Context;
            this.mapper = mapper;
        }

        public  async Task<IEnumerable<ExpensesDTO>> GetAllAsync(GeneralParams generalParams)
        {
            var query = context.Expenses.AsNoTracking();

            query = query.OrderByDescending(x => x.Date);

            if (generalParams.Sort == "asc")
            {
                query = query.OrderBy(x => x.Date);
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
                    validDates.Count == 0 || validDates.Contains(x.Date.Date));
            }


            var expenses = await query.ToListAsync();

            if (!string.IsNullOrEmpty(generalParams.Search))
            {
                var searchWords = generalParams.Search
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var textWords = searchWords
                    .Where(word => !DateTime.TryParse(word, out _))
                    .Select(word => word.ToLower())
                    .ToList();

                expenses = expenses
                    .Where(x =>
                        textWords.All(word =>
                          x.Name.ToString().ToLower().Contains(word) ||
                            x.Type.ToString().ToLower().Contains(word)
                        )
                    ).ToList();
            }


            if (generalParams.PageNumber > 0 && generalParams.PageSize > 0)
            {
                expenses = expenses
                    .Skip((generalParams.PageNumber - 1) * generalParams.PageSize)
                    .Take(generalParams.PageSize)
                    .ToList();
            }

            var result = mapper.Map<List<ExpensesDTO>>(expenses);
            return result;






        }
        // get it based on  today and based on  the monthe and on year 
        public async Task<decimal> GetTotalExpensesAsync(FilterParams filterParams)
        {

            var query = context.Expenses.AsNoTracking();

            query = query.OrderByDescending(x => x.Date);

            DateTime now = DateTime.Now;

            if (filterParams.filter == "today")
            {
                query = query.Where(x => x.Date.Date == now.Date);
            }else if(filterParams.filter == "month")
            {
                query = query.Where(x => x.Date.Month == now.Month && x.Date.Year == now.Year);
            }
            else if (filterParams.filter == "year")
            {
                query = query.Where(x => x.Date.Year == now.Year);
            }

            decimal total = await query.SumAsync(x => (decimal?)x.TotalPrice) ?? 0;



            return total;

        }
    }
    
}
