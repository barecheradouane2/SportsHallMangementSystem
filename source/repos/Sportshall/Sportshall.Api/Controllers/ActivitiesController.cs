using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Sportshall.Api.Helper;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using Sportshall.Core.Sharing;

namespace Sportshall.Api.Controllers
{
  
    public class ActivitiesController : BaseController
    {
        public ActivitiesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        // 
        public async Task<IActionResult> GetAllActivities([FromQuery] ActivitesParams activitesParams)
        {

            try
            {
                //var activities = await work.ActivitiesRepositry.GetAllAsync(activitesParams);

                var activities = await work.ActivitiesRepositry.GetAllAsync( x => x.Photos);

                if (activities is null)
                {
                    return BadRequest(new ResponseApi(400));
                }


                var totalCount = await work.ActivitiesRepositry.CountAsync();


                var activitiesDTO = mapper.Map<List<ActivitiesDTO>>(activities);





                return Ok(activitiesDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }





          
        }



        [HttpGet("get-by-id/{id}")]

        public async Task<IActionResult> GetActivityById(int id)
        {
            try
            {
                var activity = await work.ActivitiesRepositry.GetByIdAsync(id,x=>x.Photos);

                var result = mapper.Map<ActivitiesDTO>(activity);

                if (activity is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("add-activity")]

        public async Task<IActionResult> AddActivity(AddActivitiesDTO activitesDTO)
        {
            try
            {
              

                await work.ActivitiesRepositry.AddAsync(activitesDTO);

                return Ok(new ResponseApi(201, "Done"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400,ex.Message));
            }
        }

        [HttpPut("update-activity")]

        public async Task<IActionResult> UpdateActivity( UpdateActivitiesDTO activitesDTO)
        {
            try
            {

             

                await work.ActivitiesRepositry.UpdateAsync(activitesDTO);



                return Ok( new ResponseApi(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400,ex.Message));
            }
        }

        [HttpDelete("delete-activity/{id}")]

        public async Task<IActionResult> DeleteActivity(int id)
        {
            try
            {
                var activity = await work.ActivitiesRepositry.GetByIdAsync(id,x=>x.Photos);

                if (activity is null)
                {
                    return BadRequest();
                }

                await work.ActivitiesRepositry.DeleteAsync(activity);

                return Ok(new ResponseApi(200));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






        }
}
