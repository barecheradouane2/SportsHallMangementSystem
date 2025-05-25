using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Services
{
    public interface IProductSalesService
    {
        Task<ProductSalesDTO> CreateProductSales(AddProductSalesDTO productSales);

        Task<ProductSalesDTO> GetProductSalesById(int id);
        Task<List<ProductSalesDTO>> GetAllProductSales(GeneralParams generalParams);

        Task <ProductSalesDTO> DeleteProductSales(int id);

        Task<ProductSalesDTO> UpdateProductSales(UpdateProductSalesDTO productSalesDTO);


     
    }
}
