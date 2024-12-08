using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.Odbc;
using System.Data;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class IngredientCategoryRepository : IIngredientCategoryRepository
    {
        private readonly AppDbContext _context;

        public IngredientCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IngredientCategory?> GetIngredientCategoryById(int id)
        {
            return await _context.IngredientCategory.FindAsync(id);
        }

        public async Task<IEnumerable<IngredientCategory>> GetAllIngredientCategories()
        {
            return await _context.IngredientCategory.ToListAsync();
        }

        public async Task AddIngredientCategory(IngredientCategory ingredientCategory)
        {
            ingredientCategory.CreatedDate = DateTime.Now;
            ingredientCategory.ModifiedDate = DateTime.Now;

            if(ingredientCategory.Ingredient_Category_Name != null)
            {
                await _context.IngredientCategory.AddAsync(ingredientCategory);
                await _context.SaveChangesAsync();
            }
           
        }
        public async Task UpdateIngredientCategory(IngredientCategory ingredientCategory)
        {
            var existingIngredientCategory = await _context.IngredientCategory.FindAsync(ingredientCategory.Ingredient_Category_ID);
            if (existingIngredientCategory != null)
            {
                if (ingredientCategory.IsActive != null)
                {
                    existingIngredientCategory.IsActive = ingredientCategory.IsActive;
                    await _context.SaveChangesAsync(); // Lưu thay đổi vào database
                    return;
                }
                if (ingredientCategory.IsActive == null)
                {
                    existingIngredientCategory.Ingredient_Category_Name = ingredientCategory.Ingredient_Category_Name;
                    await _context.SaveChangesAsync(); // Lưu thay đổi vào database
                }

            }
        }

    }
}
