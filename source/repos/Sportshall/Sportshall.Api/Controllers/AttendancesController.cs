using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Api.Helper;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.Core.Sharing;
using System;

namespace Sportshall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : BaseController
    {
        public AttendancesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all-Attendances")]
        //[Authorize(Roles = "Admin,User,Coach")]

        public async Task<IActionResult> GetAllAsync([FromQuery]AttendancesParams attendancesParams)
        {
            try
            {

                var attendances= await work.AttendancesRepositry.GetAllAsync(attendancesParams);


                if (attendances is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var totalCount = await work.AttendancesRepositry.CountAsync();

               

                return Ok(new Pagination<AttendancesDTO>(attendancesParams.PageNumber, attendancesParams.PageSize, totalCount, attendances));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpGet("get-Attendances-by-id/{id}")]
        //[Authorize(Roles = "Admin,User,Coach")]

        public async  Task<IActionResult> GetAttendancesById(int id)
        {
            try
            {
                var attendance = await work.AttendancesRepositry.GetByIdAsync(id,x=>x.Activities,b=>b.Member);

                if (attendance is null)
                {
                    return NotFound(404);
                }

                var attendanceDTO = mapper.Map<AttendancesDTO>(attendance);

                return Ok(attendanceDTO);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add-Attendances")]
        //[Authorize(Roles = "Admin")]


        public async Task<IActionResult>  AddAttendances([FromBody] AddAttendancesDTO attendancesDTO)
        {

            try
            {

                if (attendancesDTO is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var attendances = mapper.Map<Attendances>(attendancesDTO);
                await work.AttendancesRepositry.AddAsync(attendances);






                return Ok(new ResponseApi(200, "attendances added successfully"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPut("update-Attendances")]
        //[Authorize(Roles = "Admin")] 

        public async Task<IActionResult> UpdateOffer([FromBody] UpdateAttendancesDTO attendancesDTO)
        {
            try
            {
                if (attendancesDTO is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var attendances = mapper.Map<Attendances>(attendancesDTO);

                await work.AttendancesRepositry.UpdateAsync(attendances);

                return Ok(new ResponseApi(200, "attendances updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("delete-Attendances/{id}")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteAttendances(int id)
        {
            try
            {
                var attendances = await work.AttendancesRepositry.GetByIdAsync(id);

                if (attendances is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                await work.AttendancesRepositry.DeleteAsync(id);

                return Ok(new ResponseApi(200, "Attendances deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }










    }
}
