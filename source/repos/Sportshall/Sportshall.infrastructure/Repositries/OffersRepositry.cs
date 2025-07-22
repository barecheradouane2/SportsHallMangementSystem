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
    public class OffersRepositry : GenericRepositry<Offers>, IOffersRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public OffersRepositry(AppDbContext Context, IMapper mapper) : base(Context)
        {
            context = Context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<OffersDTO>> GetAllAsync(GeneralParams generalParams)
        {
            var query = context.Offers.Include(o=>o.Activities).AsNoTracking();

            if (generalParams.Sort == "asc")
            {
                query = query.OrderBy(o => o.Name);
            }
            else if (generalParams.Sort == "desc")
            {
                query = query.OrderByDescending(o => o.Name);
            }

            if (!string.IsNullOrEmpty(generalParams.Search))
            {
                var searchWords = generalParams.Search.Split(" ");
                query = query.Where(o => searchWords.All(word => o.Name.ToLower().Contains(word.ToLower()) ||
                                                                o.Activities.Name.ToLower().Contains(word.ToLower()) ||
                                                                o.Id.ToString().Contains(word)));
            }

            if (generalParams.PageNumber > 0 && generalParams.PageSize > 0)
            {
                query = query.Skip((generalParams.PageNumber - 1) * generalParams.PageSize)
                             .Take(generalParams.PageSize);
            }

            var result = mapper.Map<IEnumerable<OffersDTO>>(query);


            return result;
        }
    }
}
