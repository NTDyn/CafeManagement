using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Newtonsoft.Json;
using System.Data.Odbc;
using System.Data;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class ProductRecipeRepository : IProductRecipeRepository
    {
        private readonly AppDbContext _context;

        public ProductRecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductRecipe>> GetAllRecipeByProductID(int id)
        {
            return await _context.ProductRecipe
                       .Where(recipe => recipe.Product_ID == id) // Lọc theo ProductId
                       .ToListAsync();
        }

        public async Task AddProductRecipe(ProductRecipe productRecipe)
        {
            productRecipe.ModifiedDate= DateTime.Now;
            productRecipe.CreatedDate = DateTime.Now;
            productRecipe.IsActive= true;
            await _context.ProductRecipe.AddAsync(productRecipe);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductRecipe(ProductRecipe productRecipe)
        {
            var existingProductRecipe = await _context.ProductRecipe.FindAsync(productRecipe.Recipe_ID);
            if (existingProductRecipe != null)
            {
                if (productRecipe.IsActive != null)
                {
                    existingProductRecipe.IsActive = productRecipe.IsActive;
                    existingProductRecipe.ModifiedDate = DateTime.Now;
                }
                if(productRecipe.Unit != null)
                {
                    existingProductRecipe.Unit= productRecipe.Unit;
                    existingProductRecipe.ModifiedDate = DateTime.Now;
                }
                if(productRecipe.Quantity != null)
                {
                    existingProductRecipe.Quantity = productRecipe.Quantity;
                    existingProductRecipe.ModifiedDate = DateTime.Now;
                }
                await _context.SaveChangesAsync();
                

            }
        }

    }
}
