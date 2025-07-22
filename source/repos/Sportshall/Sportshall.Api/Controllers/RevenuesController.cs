using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Api.Helper;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.Core.Sharing;

namespace Sportshall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenuesController : BaseController
    {
        public RevenuesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all-revenues")]
        public async Task<IActionResult> GetAllRevenues([FromQuery] GeneralParams generalParams)
        {
            try
            {
                var revenuesDTO = await work.RevenuesRepositry.GetAllAsync(generalParams);

                if (revenuesDTO is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var totalCount = await work.RevenuesRepositry.CountAsync();

                return Ok(new Pagination<RevenuesDTO>(generalParams.PageNumber, generalParams.PageSize, totalCount, revenuesDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-total-revenue")]

        public async Task<IActionResult> GetTotalRevenue([FromQuery] FilterParams filterParams)
        {
            try
            {
                var totalRevenue = await work.RevenuesRepositry.GetTotalRevenueAsync(filterParams);

               

                return Ok(totalRevenue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("create-revenue")]
        public async Task<IActionResult> AddRevenue([FromBody] AddRevenuesDTO addRevenuesDTO)
        {
            try
            {
                
                if (addRevenuesDTO is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var revenue = mapper.Map<Sportshall.Core.Entites.Revenues>(addRevenuesDTO);
                await work.RevenuesRepositry.AddAsync(revenue);

                return Ok(new ResponseApi(200, "revenue added successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-revenue")]


        public async Task<IActionResult> UpdateRevenue([FromBody] UpdateRevenuesDTO updateRevenuesDTO)
        {
            try
            {
                if (updateRevenuesDTO is null)
                {
                    return BadRequest(new ResponseApi(400));
                }


               var revenue=mapper.Map<Revenues>(updateRevenuesDTO);
                await work.RevenuesRepositry.UpdateAsync(revenue);

                return Ok(new ResponseApi(200, "revenue updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete-revenue/{id}")]
        public async Task<IActionResult> DeleteRevenue(int id)
        {
            try
            {
                var revenue = await work.RevenuesRepositry.GetByIdAsync(id);
                if (revenue is null)
                {
                    return NotFound(new ResponseApi(404, "Revenue not found"));
                }

                await work.RevenuesRepositry.DeleteAsync(id);

                return Ok(new ResponseApi(200, "Revenue deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-revenue-by-id/{id}")]
        public async Task<IActionResult> GetRevenueById(int id)
        {
            try
            {
                var revenue = await work.RevenuesRepositry.GetByIdAsync(id);
                if (revenue is null)
                {
                    return NotFound(new ResponseApi(404, "Revenue not found"));
                }

                var revenueDTO = mapper.Map<RevenuesDTO>(revenue);
                return Ok(revenueDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        }
   
}
