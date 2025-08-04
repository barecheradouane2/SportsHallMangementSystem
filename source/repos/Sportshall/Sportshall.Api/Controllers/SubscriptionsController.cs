using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Api.Helper;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.Services;
using Sportshall.Core.Sharing;

namespace Sportshall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {

        private readonly ISubscriptionService _subscriptionService;


        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;

        }

        [HttpPost("add-subscription")]
        [Authorize(Roles = "Admin,User,Coach")]

        public  async Task <IActionResult>  AddSubscription(AddSubscriptionsDTO addSubscriptionsDTO)
        {

            try
            {
                var subscriptiondto = await _subscriptionService.AddSubscriptionAsync(addSubscriptionsDTO);

                if (subscriptiondto == null)
                {
                    return BadRequest(new ResponseApi(400));

                }

                return Ok(subscriptiondto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

         
        }


        [HttpGet("getall-subsctipton")]
        [Authorize(Roles = "Admin,User,Coach")]


        public async Task<IActionResult> GetAllSubsctiption([FromQuery] SubscriptionsParams subscriptionsParams)
        {

            try
            {
                var subscriptiondtolist = await _subscriptionService.GetAllSubscription(subscriptionsParams);

                if (subscriptiondtolist is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

               // var totalCount = subscriptiondtolist.Count();

                var totalCount = await _subscriptionService.CountAsync(subscriptionsParams);


                return Ok(new Pagination<SubscriptionsDTO>(subscriptionsParams.PageNumber, subscriptionsParams.PageSize, totalCount, subscriptiondtolist));

              

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        [HttpGet("get-subscription-stats")]
        [Authorize(Roles = "Admin,User,Coach")]
        public async Task<IActionResult> GetSubscriptionStats()
        {
            try
            {
                var stats = await _subscriptionService.GetSubscriptionstats();

                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getall-total-subsctipton")]
        [Authorize(Roles = "Admin,User,Coach")]
        public async Task<IActionResult> GetTotalSubscription([FromQuery] FilterParams filterParams)
        {
            try
            {
                var totalSubscription = await _subscriptionService.GetTotalSubscriptionAsync(filterParams);

                return Ok(totalSubscription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("get-subsctipton-by-id/{id}")]
        [Authorize(Roles = "Admin,User,Coach")]


        public async Task<IActionResult> GetOneSubscription(int id)
        {
            try
            {
                var subscripitiondto = await _subscriptionService.GetSubscriptionAsync(id);

                if (subscripitiondto ==null)
                {
                    return NotFound();
                }

                return Ok(subscripitiondto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("delete-subscription/{id}")]

        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> Deletesubscription(int id)
        {
            try
            {
                var subscriptionsdto = await _subscriptionService.DeleteSubscriptionAsync(id);

                if (subscriptionsdto is null)
                {
                    return BadRequest();
                }

              

                return Ok(subscriptionsdto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-subscription")]
        [Authorize(Roles = "Admin,User,Coach")]


        public async Task<IActionResult> Updatesubscription(UpdateSubscriptionsDTO updateSubscriptionsDTO)
        {
            try
            {



                var updatesubscriptionsDTO = await _subscriptionService.UpdateSubscriptionAsync(updateSubscriptionsDTO);



                return Ok(updatesubscriptionsDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }












    }
}
