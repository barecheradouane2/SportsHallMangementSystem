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
    public class OffersController : BaseController
    {
        public OffersController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all-offers")]
        public async Task<IActionResult> GetAllOffers([FromQuery] GeneralParams activitesParams)
        {
            try
            {
                //var offers = await work.OffersRepositry.GetAllAsync(activitesParams);

                var offers = await work.OffersRepositry.GetAllAsync( o => o.Activities);

                if (offers is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var totalCount = await work.OffersRepositry.CountAsync();

                var offersDTOList = mapper.Map<IEnumerable<OffersDTO>>(offers);




                return Ok(offersDTOList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-offer-by-id/{id}")]

        public async Task<IActionResult> GetOfferById(int id)
        {
            try
            {
                var offer = await work.OffersRepositry.GetByIdAsync(id, o => o.Activities);

                if (offer is null)
                {
                    return NotFound(new ResponseApi(404));
                }

                var offerDTO = mapper.Map<OffersDTO>(offer);

                return Ok(offerDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add-offer")]

        public async Task<IActionResult> AddOffer([FromBody] AddOffersDTO offersDTO)
        {
            try
            {

              


                if (offersDTO is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var offer = mapper.Map<Offers>(offersDTO);

                await work.OffersRepositry.AddAsync(offer);

                

               
                

                return Ok(new ResponseApi(200, "Offer added successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("update-offer")]

        public async Task<IActionResult> UpdateOffer([FromBody] AddOffersDTO offersDTO)
        {
            try
            {
                if (offersDTO is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var offer = mapper.Map<Offers>(offersDTO);

                await work.OffersRepositry.UpdateAsync(offer);

                return Ok(new ResponseApi(200, "Offer updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete-offer/{id}")]

        public async Task<IActionResult> DeleteOffer(int id)
        {
            try
            {
                var offer = await work.OffersRepositry.GetByIdAsync(id);

                if (offer is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                await work.OffersRepositry.DeleteAsync(id);

                return Ok(new ResponseApi(200, "Offer deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
    }
