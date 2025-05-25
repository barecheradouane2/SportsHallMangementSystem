using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Api.Helper;
using Sportshall.Core.DTO;
using Sportshall.Core.Services;
using Sportshall.Core.Sharing;
using Sportshall.infrastructure.Repositries.Service;

namespace Sportshall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSalesController : ControllerBase
    {

        private readonly IProductSalesService _productSalesService;

        public ProductSalesController(IProductSalesService productSalesService)
        {
            _productSalesService = productSalesService;
        }

        [HttpPost("create-product-sales")]

        public async Task<IActionResult> CreateProductSales(AddProductSalesDTO addProductSalesDTO)
        {
            try
            {
                var productSalesDto = await _productSalesService.CreateProductSales(addProductSalesDTO);

                if (productSalesDto == null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                return Ok(productSalesDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("get-all-product-sales")]
        public async Task<IActionResult> GetAllProductSales( [FromQuery] GeneralParams generalParams)
        {
            try
            {
                var productSales = await _productSalesService.GetAllProductSales(generalParams);

                if (productSales == null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                var totalCount = productSales.Count;

                return Ok(new Pagination<ProductSalesDTO>(generalParams.PageNumber, generalParams.PageSize, totalCount, productSales));

               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-product-sales-by-id/{id}")]
        public async Task<IActionResult> GetProductSalesById(int id)
        {
            try
            {
                var productSales = await _productSalesService.GetProductSalesById(id);

                if (productSales == null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                return Ok(productSales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-product-sales")]
        public async Task<IActionResult> UpdateProductSales(UpdateProductSalesDTO updateProductSalesDTO)
        {
            try
            {
                var productSales = await _productSalesService.UpdateProductSales(updateProductSalesDTO);

                if (productSales == null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                return Ok(productSales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete-product-sales/{id}")]

        public async Task<IActionResult> DeleteProductSales(int id)
        {
            try
            {
                var productSales = await _productSalesService.DeleteProductSales(id);

                if (productSales == null)
                {
                    return BadRequest(new ResponseApi(400));
                }

                return Ok(productSales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        }
}
