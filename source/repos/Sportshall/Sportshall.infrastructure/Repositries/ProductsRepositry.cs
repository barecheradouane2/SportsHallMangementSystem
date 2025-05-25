using AutoMapper;
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

namespace Sportshall.infrastructure.Repositries
{
    public class ProductsRepositry : GenericRepositry<Products>, IProductsRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        private readonly IImageMangementService imageMangementService;
        public ProductsRepositry(AppDbContext Context, IMapper mapper, IImageMangementService imageMangementService) : base(Context)
        {
            this.context = Context;
            this.mapper = mapper;
            this.imageMangementService = imageMangementService;
        }

        public async Task<bool> AddAsync(AddProductsDTO addproductsdto)
        {
            if (addproductsdto == null)
            {
                return false;
            }

            var products = mapper.Map<Products>(addproductsdto);

            var expenses = new Expenses (Expensestype.oneTime, addproductsdto.Name, addproductsdto.OldPrice, DateTime.Today, addproductsdto.Note);

            await context.Expenses.AddAsync(expenses);

           

            await context.Products.AddAsync(products);
            await context.SaveChangesAsync();


            if (addproductsdto.Photos != null && addproductsdto.Photos.Any())
            {
                var imagepath = await imageMangementService.AddImageAsync(addproductsdto.Photos, addproductsdto.Name);

            var photo = imagepath.Select(x => new ProductPhoto
            {
                ImageName = x,
                ProductID = products.Id
            }).ToList();

            await context.ProductPhoto.AddRangeAsync(photo);

            await context.SaveChangesAsync();

            }

            return true;


        }

        public async Task<bool> AddQuantityAsync(AddQtyToProduct addQtyToProduct)
        {
           var product =  await context.Products.FirstOrDefaultAsync(x => x.Id == addQtyToProduct.Id);

            
            var expense = await context.Expenses.FirstOrDefaultAsync(x => x.Name == product.Name);

            if ( addQtyToProduct.StockQty < 0)
            {
                return false;
            }


            product.StockQty += addQtyToProduct.StockQty;


            context.Products.Update(product);

        

            expense.TotalPrice += addQtyToProduct.OldPrice;
            expense.Date = DateTime.Today;

            context.Expenses.Update(expense);


            await context.SaveChangesAsync();





            return true;
        }

        public async Task<bool> DeleteAsync(Products products)
        {

            var photo = await context.ProductPhoto.Where(x => x.ProductID == products.Id).ToListAsync();

            foreach (var item in photo)
            {
                imageMangementService.DeleteImageAsync(item.ImageName);
            }


            context.Products.Remove(products);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProductsDTO>> GetAllAsync(GeneralParams generalParams)
        { 
            var query = context.Products.Include(x => x.Photos).AsNoTracking();

            if (generalParams.Sort == "asc")
            {
                query = query.OrderBy(x => x.Name);
            }
            else if (generalParams.Sort == "desc")
            {
                query = query.OrderByDescending(x => x.Name);
            }

            if (!string.IsNullOrEmpty(generalParams.Search))
            {
                var searchWords = generalParams.Search.Split(" ");

                query = query.Where(x => searchWords.All(word => x.Name.ToLower().Contains(word.ToLower()) || x.Note.ToLower().Contains(word.ToLower())));


            }

            if (generalParams.PageNumber > 0 && generalParams.PageSize > 0)
            {
                query = query.Skip((generalParams.PageNumber - 1) * generalParams.PageSize).Take(generalParams.PageSize);
            }


            var result = mapper.Map<List<ProductsDTO>>(query);

            return result;





        }

        public async Task<bool> UpdateAsync(UpdateProductsDTO productsdto)
        {

            if (productsdto == null)
            {
                return false;
            }

            var Findactivities = await context.Products.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == productsdto.Id);

            if (Findactivities == null)
            {
                return false;
            }

           

            mapper.Map(productsdto, Findactivities);

          // update the expense 

            var expense = await context.Expenses.FirstOrDefaultAsync(x => x.Name == productsdto.Name);

            expense.Type = Expensestype.oneTime;
            expense.TotalPrice = productsdto.OldPrice;
            expense.Date = DateTime.Today;
            expense.Note = productsdto.Note;

            context.Expenses.Update(expense);

            await context.SaveChangesAsync();





            var FindPhoto = await context.ProductPhoto.Where(x => x.ProductID == productsdto.Id).ToListAsync();


            foreach (var item in FindPhoto)
            {
                imageMangementService.DeleteImageAsync(item.ImageName);
            }

            context.ProductPhoto.RemoveRange(FindPhoto);



            var imagepath = await imageMangementService.AddImageAsync(productsdto.Photos, productsdto.Name);

            var photo = imagepath.Select(x => new ProductPhoto
            {
                ImageName = x,
                ProductID = productsdto.Id
            }).ToList();

            await context.ProductPhoto.AddRangeAsync(photo);

            await context.SaveChangesAsync();


















            

            return true;



        }
    }

}
