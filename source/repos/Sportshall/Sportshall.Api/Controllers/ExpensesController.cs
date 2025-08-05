using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class ExpensesController : BaseController
    {
        public ExpensesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all-expenses")]
        //[Authorize(Roles = "Admin,User,Coach")]


        public async Task<IActionResult> GetAllExpenses([FromQuery] GeneralParams generalParams)
        {
            try
            {
                var expensesDTOList = await work.ExpensesRepositry.GetAllAsync(generalParams);

                if (expensesDTOList is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var totalCount = await work.ExpensesRepositry.CountAsync();

               



                return Ok(new Pagination<ExpensesDTO>(generalParams.PageNumber,generalParams.PageSize,totalCount, expensesDTOList));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-total-expenses")]
        //[Authorize(Roles = "Admin,User,Coach")]
        public async Task<IActionResult> GetTotalExpenses([FromQuery] FilterParams filterParams)
        {
            try
            {
                var totalExpenses = await work.ExpensesRepositry.GetTotalExpensesAsync(filterParams);

               

                return Ok(totalExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("get-expenses-by-id/{id}")]
        //[Authorize(Roles = "Admin,User,Coach")]

        public async Task<IActionResult> GetExpensesById(int id)
        {
            try
            {
                var expenses = await work.ExpensesRepositry.GetByIdAsync(id);

                if (expenses is null)
                {
                    return NotFound(new ResponseApi(404));
                }

                var expensesDTO = mapper.Map<ExpensesDTO>(expenses);

                return Ok(expensesDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-expenses")]
        //[Authorize(Roles = "Admin,User,Coach")] 
        public async Task<IActionResult> AddExpenses([FromBody] AddExpensesDTO expensesDTO)
        {
            try
            {
                if (expensesDTO is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var expenses = mapper.Map<Expenses>(expensesDTO);

                await work.ExpensesRepositry.AddAsync(expenses);
            

                return Ok(new ResponseApi(200));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("update-expenses/{id}")]
        //[Authorize(Roles = "Admin,User,Coach")]

        public async Task<IActionResult> UpdateExpenses(int id, [FromBody] UpdateExpensesDTO expensesDTO)
        {
            try
            {
                if (expensesDTO is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var expenses = await work.ExpensesRepositry.GetByIdAsync(id);

                if (expenses is null)
                {
                    return NotFound(new ResponseApi(404));
                }


                var expensestpupdate = mapper.Map<Expenses>(expensesDTO);

                await work.ExpensesRepositry.UpdateAsync(expensestpupdate);

                return Ok(new ResponseApi(200,"done"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-expenses/{id}")]
        //[Authorize(Roles = "Admin")]


        public async Task<IActionResult> DeleteExpenses(int id)
        {
            try
            {
                var expenses = await work.ExpensesRepositry.GetByIdAsync(id);

                if (expenses is null)
                {
                    return NotFound(new ResponseApi(404));
                }

                await work.ExpensesRepositry.DeleteAsync(id);

                return Ok(new ResponseApi(200, "Offer deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }













        }
}
