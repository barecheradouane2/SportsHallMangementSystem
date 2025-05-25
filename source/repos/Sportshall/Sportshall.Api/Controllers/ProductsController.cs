using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Api.Helper;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.Core.Services;
using Sportshall.Core.Sharing;
using Sportshall.infrastructure.Repositries;

namespace Sportshall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        //private readonly IProductsRepositry productsRepositry;
        //private readonly IMapper mapper;
        //private readonly IImageMangementService imageMangementService;

        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
            
        }


        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAll([FromQuery] GeneralParams generalParams)
        {
            try
            {
                var products = await work.ProductsRepositry.GetAllAsync(d=>d.Photos);

                if (products is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var totalCount = await work.ProductsRepositry.CountAsync();
                var productsDTO = mapper.Map<IReadOnlyList<ProductsDTO>>(products);
                return Ok(new Pagination<ProductsDTO>(generalParams.PageNumber, generalParams.PageSize, totalCount, productsDTO ));

            }
            catch (Exception ex)
            {

              return BadRequest(ex.Message);
            }
            
           
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductsDTO addproductsdto)

        {
            try
            {
                var product = await work.ProductsRepositry.AddAsync(addproductsdto);

                if (!product)
                {
                    return BadRequest(new ResponseApi(400, "Failed to create product"));
                }

                    return Ok(new ResponseApi(201, "Done"));

                
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }

        [HttpPost("add-product-qunatity")]
        public async Task<IActionResult> AddProductQuntity([FromBody] AddQtyToProduct addProductQuntityDTO)
        {
            try
            {
                var product = await work.ProductsRepositry.AddQuantityAsync(addProductQuntityDTO);

                if (!product)
                {
                    return BadRequest(new ResponseApi(400, "Failed to create product"));
                }

                return Ok(new ResponseApi(200, "Done"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }


        [HttpPut("update-product")]

        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductsDTO updateProductsDTO)
        {
            try
            {
                var product = await work.ProductsRepositry.UpdateAsync(updateProductsDTO);

                if (!product)
                {
                    return BadRequest(new ResponseApi(400, "Failed to update product"));
                }

                return Ok(new ResponseApi(200, "Done"));

            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await work.ProductsRepositry.GetByIdAsync(id,x=> x.Photos);

                if (product is null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                await work.ProductsRepositry.DeleteAsync(product);

                return Ok(new ResponseApi(200, "Done"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("get-by-id/{id}")]

        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await work.ProductsRepositry.GetByIdAsync(id, x => x.Photos);




                var result = mapper.Map<ProductsDTO>(product);

                if (product is null)
                {
                    return NotFound(new ResponseApi(400));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }





    }
   
}
