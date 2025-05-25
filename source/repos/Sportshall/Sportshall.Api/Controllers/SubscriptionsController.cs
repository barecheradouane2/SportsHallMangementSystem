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


        public async Task<IActionResult> GetAllSubsctiption(SubscriptionsParams subscriptionsParams)
        {

            try
            {
                var subscriptiondtolist = await _subscriptionService.GetAllSubscription(subscriptionsParams);

                if (subscriptiondtolist is null)
                {
                    return BadRequest(new ResponseApi(400));
                }


                var totalCount = await _subscriptionService.CountAsync();


                return Ok(new Pagination<SubscriptionsDTO>(subscriptionsParams.PageNumber, subscriptionsParams.PageSize, totalCount, subscriptiondtolist));

              

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }


        

        [HttpGet("get-subsctipton-by-id/{id}")]


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
