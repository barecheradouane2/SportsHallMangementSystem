using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Core.interfaces;

namespace Sportshall.Api.Controllers
{
 
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("not-found")]
        public async Task<IActionResult>  GetNotFound()
        {

            var activities = await work.ActivitiesRepositry.GetByIdAsync(100);

            if(activities is null)
            {
                return NotFound(new { message = "Activity not found" });
            }
            else
            {
                return Ok(activities);
            }



        }

        [HttpGet("server-error")]

        public async Task<IActionResult> ServerError()
        {

            var activities = await work.ActivitiesRepositry.GetByIdAsync(100);

            activities.Name = "";

            return Ok(activities);

        }


        [HttpGet("bad-request/{Id}")]
        public async Task<IActionResult> GetBadRequest(int Id)
        {
            var activities = await work.ActivitiesRepositry.GetByIdAsync(100);

            if (activities is null)
            {
                return BadRequest(new { message = "Activity not found" });
            }
            else
            {
                return Ok(activities);
            }

        }

    }
}
