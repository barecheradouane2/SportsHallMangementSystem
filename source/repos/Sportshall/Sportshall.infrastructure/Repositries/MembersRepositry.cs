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
    public class MembersRepositry : GenericRepositry<Members>, IMembersRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private IMapper mapper1;

        public MembersRepositry(AppDbContext Context, IMapper mapper) : base(Context)
        {
            context = Context;
            this.mapper = mapper;
        }

        public  async Task<IEnumerable<MembersDTO>> GetAllAsync(GeneralParams generalParams)
        {

            var query = context.Members.AsNoTracking();

            if(generalParams.Sort == "asc")
            {
                query = query.OrderBy(x => x.FullName);
            }else if (generalParams.Sort == "desc")
            {
                query =query.OrderByDescending(x => x.FullName);

            }

            if (!string.IsNullOrEmpty(generalParams.Search))
            {
                var searchWords = generalParams.Search.Split(" ");

                query = query.Where(x => searchWords.All(word => x.FullName.ToLower().Contains(word.ToLower()) || x.PhoneNumber.ToLower().Contains(word.ToLower()) ||
                x.Id.ToString().Contains(word)
                ))
                    
                    ;


            }

            if (generalParams.PageNumber > 0 && generalParams.PageSize > 0)
            {
                query = query.Skip((generalParams.PageNumber - 1) * generalParams.PageSize)
                             .Take(generalParams.PageSize);
            }

           

            var result = mapper.Map<List<MembersDTO>>(query);

            return result;







        }
    }
}
