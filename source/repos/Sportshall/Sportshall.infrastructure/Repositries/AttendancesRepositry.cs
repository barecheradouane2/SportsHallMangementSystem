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
                var searchWords = attendancesParams.Search
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
                    (validDates.Count == 0 || validDates.Contains(x.AttendanceDate.Date)) &&
                    textWords.All(word =>
                        x.Member.FullName.ToLower().Contains(word) ||
                        x.Activities.Name.ToLower().Contains(word) ||
                        x.Status.ToString().ToLower().Contains(word)
                    ));
            }




            if (attendancesParams.PageNumber > 0 && attendancesParams.PageSize > 0)
            {
                query = query.Skip((attendancesParams.PageNumber - 1) * attendancesParams.PageSize)
                             .Take(attendancesParams.PageSize);
            }







            var result = mapper.Map<List<AttendancesDTO>>(query);

            return result;











        }
    }
}
