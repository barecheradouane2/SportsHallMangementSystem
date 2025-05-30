﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Api.Helper;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.Core.Sharing;

namespace Sportshall.Api.Controllers
{
    
    public class MembersController : BaseController
    {
        public MembersController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }


        [HttpGet("get-all-Members")]
        public async Task<IActionResult> GetAllMembers([FromQuery] GeneralParams generalParams)
        {
            try
            {
                var members = await work.MembersRepositry.GetAllAsync(generalParams);

                if (members is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                return Ok(members);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-Member-by-id")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            try
            {
                var member = await work.MembersRepositry.GetByIdAsync(id);

                if (member is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                return Ok(member);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-Member")]
        public async Task<IActionResult> AddMember([FromBody] AddMembersDTO member)
        {
            try
            {
                if (member is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var newMember = mapper.Map<Sportshall.Core.Entites.Members>(member);
                await work.MembersRepositry.AddAsync(newMember);
             

                return Ok(new ResponseApi(200, "Member added successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-Member")]


        public async Task<IActionResult> UpdateMember([FromBody] AddMembersDTO memberdto)
        {

            try
            {
                if (memberdto is null)
                {
                    return BadRequest(new ResponseApi(400));
                }


                var member= mapper.Map<Members>(memberdto);

                await work.MembersRepositry.UpdateAsync(member);

                return Ok(new ResponseApi(200, "member updated successfully"));


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete-Member/{id}")]


        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            {
                var member = await work.MembersRepositry.GetByIdAsync(id);

                if (member is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                await work.MembersRepositry.DeleteAsync(id);

                return Ok(new ResponseApi(200, "Member deleted successfully"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }










    }
    
}
