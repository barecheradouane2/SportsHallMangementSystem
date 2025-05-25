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
    public class AttendancesRepositry : GenericRepositry<Attendances>, IAttendancesRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public AttendancesRepositry(AppDbContext context, AutoMapper.IMapper _mapper) : base(context)
        {
            this.context = context;
            this.mapper = _mapper;
        }

        public  async Task<IEnumerable<AttendancesDTO>> GetAllAsync(AttendancesParams attendancesParams)
        {


            var query = context.Attendances.Include(x=>x.Member).Include(m=>m.Activities).AsNoTracking();


            if (attendancesParams.Sort == "asc")
            {
                query = query.OrderBy(x => x.AttendanceDate);
            }
            else if (attendancesParams.Sort == "desc")
            {
                query = query.OrderByDescending(x => x.AttendanceDate);
            }


            if (!string.IsNullOrEmpty(attendancesParams.Search))
            {
                var searchWords = attendancesParams.Search.Split(" ");


                query = query.Where(x =>
                searchWords.Any(word =>
                  x.AttendanceDate.ToString("yyyy-MM-dd").Contains(word)));




            }

            if (!string.IsNullOrWhiteSpace(attendancesParams.MemberFullName))
            {
                query = query.Where(x => x.Member.FullName.ToLower() == attendancesParams.MemberFullName.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(attendancesParams.ActivityName))
            {
                query = query.Where(x => x.Activities.Name.ToLower() == attendancesParams.ActivityName.ToLower());
            }







            var result = mapper.Map<List<AttendancesDTO>>(query);

            return result;











        }
    }
}
