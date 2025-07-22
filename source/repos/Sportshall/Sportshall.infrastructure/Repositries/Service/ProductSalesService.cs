using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.Core.Services;
using Sportshall.Core.Sharing;
using Sportshall.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries.Service
{
    public class ProductSalesService : IProductSalesService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductSalesService(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductSalesDTO> CreateProductSales(AddProductSalesDTO productSales)
        {
            List<ProductSalesItem> productSalesItemslist = new List<ProductSalesItem>();

            foreach (var item in productSales.ProductSalesItems)
            {
                var product = await _unitOfWork.ProductsRepositry.GetByIdAsync(item.ProductsID);

                product.StockQty -= item.qty;

                var productSalesItem = new ProductSalesItem(
                    product.Id,
                    item.qty,
                    product.Name,
                   ( item.qty * product.NewPrice) / product.BaseQty
                );

                productSalesItemslist.Add(productSalesItem);
            }

            var subtotal = productSalesItemslist.Sum(x => x.TotalPrice);

            bool isFullPaid = subtotal == productSales.ClientPayement;

            var productsales = new ProductSales(
                totalPrice: subtotal,
                clientPayement: productSales.ClientPayement,
                isFullPaid: isFullPaid,
                membersID: productSales.MembersID
            );

            productsales.ProductSalesItems = productSalesItemslist;

            await _context.ProductSales.AddAsync(productsales);
            await _context.SaveChangesAsync();

            string membername = "";

            if (productSales.MembersID != null)
            {
                var member= await _context.Members.FirstOrDefaultAsync(m => m.Id == productSales.MembersID);
                 membername = member?.FullName ?? "client";

            }

            Revenues revenues = new Revenues(
                RevenueType.productSales,
                productsales.Id,
                subtotal,
            DateTime.Now,
                $"Product Sales for {membername} with total price {subtotal}"
            );

            await _context.Revenues.AddAsync(revenues);

            await _context.SaveChangesAsync();

            var result = _mapper.Map<ProductSalesDTO>(productsales); 

            return result;
        }


        public async Task<ProductSalesDTO> DeleteProductSales(int id)
        {

            var productsales=  await _context.ProductSales.FirstOrDefaultAsync(x=>x.Id == id);

            if(productsales == null) return null;

            var result =  _mapper.Map<ProductSalesDTO>(productsales);


             _context.ProductSales.Remove(productsales);

            var revenues = await _context.Revenues
                .FirstOrDefaultAsync(x => x.RelatedId == productsales.Id && x.RevenueType == RevenueType.productSales);

            if (revenues != null)
            {
                _context.Revenues.Remove(revenues);
            }



            await _context.SaveChangesAsync();


            return result;

            
        }

        public async Task<List<ProductSalesDTO>> GetAllProductSales(GeneralParams generalParams)
        {
            var query = _context.ProductSales
                .Include(x => x.ProductSalesItems)
                .Include(m => m.Members)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(generalParams.Search))
            {
                query = query.Where(x =>
                    x.Members.FullName.Contains(generalParams.Search) ||
                    x.ProductSalesItems.Any(p => p.ProductName.Contains(generalParams.Search)));
            }

            if (generalParams.PageNumber > 0 && generalParams.PageSize > 0)
            {
                query = query
                    .Skip((generalParams.PageNumber - 1) * generalParams.PageSize)
                    .Take(generalParams.PageSize);
            }

            var entities = await query.ToListAsync();
            var result = _mapper.Map<List<ProductSalesDTO>>(entities);

            return result;
        }


        public async Task<ProductSalesDTO> GetProductSalesById(int id)
        {
            var productSales = await _context.ProductSales
                .Include(x => x.ProductSalesItems)
                .Include(m => m.Members)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (productSales == null)
            {
                throw new Exception("Product Sales not found");
            }

            var result = _mapper.Map<ProductSalesDTO>(productSales);
            return result;
        }


        public async Task<ProductSalesDTO> UpdateProductSales(UpdateProductSalesDTO productSalesDTO)
        {

            var existingProductSales = await _context.ProductSales.Include(x => x.ProductSalesItems).Include(m => m.Members).FirstOrDefaultAsync(x => x.Id == productSalesDTO.ID);

            if (existingProductSales == null)
            {

                return null;
            }

            _mapper.Map(productSalesDTO, existingProductSales);

            string membername = "";
            if (existingProductSales.MembersID != null)
            {
                var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == existingProductSales.MembersID);
                membername = member?.FullName ?? "client";

            }

            var revenues = await _context.Revenues
              .FirstOrDefaultAsync(x => x.RelatedId == existingProductSales.Id && x.RevenueType == RevenueType.productSales);

            if (revenues != null)
            {
                revenues.Amount = existingProductSales.TotalPrice;
                revenues.RevenueDate = DateTime.Now;
                revenues.Note = $"Updated Product Sales for {membername} with total price {existingProductSales.TotalPrice}";
                _context.Revenues.Update(revenues);
            }












            await _context.SaveChangesAsync();

            var result = _mapper.Map<ProductSalesDTO>(existingProductSales);

            return result;


        }
    }
    
}
