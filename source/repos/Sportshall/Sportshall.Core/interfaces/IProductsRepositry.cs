using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.interfaces
{
    public interface IProductsRepositry : IGenericRepositry<Products>
    {
        Task<IEnumerable<ProductsDTO>> GetAllAsync(GeneralParams generalParams);
        Task<bool> AddAsync(AddProductsDTO addproductsdto);

        Task<bool> UpdateAsync(UpdateProductsDTO updateProductsDTO);

        Task<bool> DeleteAsync(Products products);

        Task<bool> AddQuantityAsync(AddQtyToProduct  addQtyToProduct);

    }
    
}
