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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries
{
    public class ActivitiesRepositry : GenericRepositry <Activities>,IActivitiesRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        private readonly IImageMangementService imageMangementService;
        public ActivitiesRepositry(AppDbContext Context, IMapper mapper, IImageMangementService imageMangementService) : base(Context)
        {
            context = Context;
            this.mapper = mapper;
            this.imageMangementService = imageMangementService;
        }



        public async Task<bool> AddAsync(AddActivitiesDTO activitiesDTO)
        {
            if(activitiesDTO == null)
            {
                return false;
            }

            var activities = mapper.Map<Activities>(activitiesDTO);

           await context.Activities.AddAsync(activities);

          await context.SaveChangesAsync();

            var imagepath = await imageMangementService.AddImageAsync(activitiesDTO.Photos, activitiesDTO.Name);

            var photo=imagepath.Select(x => new Photo
            {
                ImageName = x,
                ActivitiesID = activities.Id
            }).ToList();

           await context.Photo.AddRangeAsync(photo);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(UpdateActivitiesDTO updateactivitiesDTO)
        {
           if(updateactivitiesDTO == null)
            {
                return false;
            }

            var Findactivities = await context.Activities.Include( x => x.Photos).FirstOrDefaultAsync(x => x.Id == updateactivitiesDTO.Id);

            if (Findactivities == null)
            {
                return false;
            }

            mapper.Map(updateactivitiesDTO, Findactivities);


            var FindPhoto = await context.Photo.Where(x => x.ActivitiesID == updateactivitiesDTO.Id).ToListAsync();


            foreach (var item in FindPhoto)
            {
                imageMangementService.DeleteImageAsync(item.ImageName);
            }

            context.Photo.RemoveRange(FindPhoto);



            var imagepath = await imageMangementService.AddImageAsync(updateactivitiesDTO.Photos, updateactivitiesDTO.Name);

            var photo = imagepath.Select(x => new Photo
            {
                ImageName = x,
                ActivitiesID = updateactivitiesDTO.Id
            }).ToList();

           await context.Photo.AddRangeAsync(photo);

            await context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(Activities activities)
        {


            var photo = await context.Photo.Where(x => x.ActivitiesID == activities.Id).ToListAsync();

            foreach(var item in photo)
            {
                imageMangementService.DeleteImageAsync(item.ImageName);
            }


            context.Activities.Remove(activities);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ActivitiesDTO>> GetAllAsync(ActivitesParams activitesParams)
        {

            var query = context.Activities.Include(x=>x.Photos).AsNoTracking();

        

            if (activitesParams.Sort == "asc")
            {
                query = query.OrderBy(x => x.Name);
            }
            else if (activitesParams.Sort == "desc")
            {
                query = query.OrderByDescending(x => x.Name);
            }

            if (!string.IsNullOrEmpty(activitesParams.Search))
            {
                var searchWords = activitesParams.Search.Split(" ");

                query = query.Where(x => searchWords.All(word => x.Name.ToLower().Contains(word.ToLower()) || x.Description.ToLower().Contains(word.ToLower())));


            }

            if (activitesParams.PageNumber  > 0 && activitesParams.PageSize > 0)
            {
                query = query.Skip((activitesParams.PageNumber - 1) * activitesParams.PageSize).Take(activitesParams.PageSize);
            }


            var result = mapper.Map<List<ActivitiesDTO>>(query);

            return result;


        }
    }
}
