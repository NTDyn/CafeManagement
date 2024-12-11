using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class RecipeRawRepository : IRecipeRawRepository
    {
        private readonly AppDbContext _context;
        public RecipeRawRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RecipeRaw>> GetRecipeRaw(Nullable<int> ingredientResult)
        {
            List<RecipeRaw> RecipeRawList = null;
            Expression<Func<RecipeRaw, bool>> _Filter = r => true;

            if (ingredientResult != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Ingredient_Result == ingredientResult);
            }


            RecipeRawList = await _context.RecipeRaw.Where(_Filter).ToListAsync();

            return RecipeRawList;
        }


        public async Task AddRecipeRaw(RecipeRaw recipeRaw)
        {
            recipeRaw.CreatedDate = DateTime.Now;
            recipeRaw.ModifiedDate = DateTime.Now;
            recipeRaw.IsActive = true;

            await _context.RecipeRaw.AddAsync(recipeRaw);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRecipeRaw(RecipeRaw recipeRaw)
        {
            var existingRecipeRaw = await _context.RecipeRaw.FindAsync(recipeRaw.Recipe_ID);
            if (existingRecipeRaw != null)
            {
                if (recipeRaw.Ingredient_Raw != null)
                {
                    existingRecipeRaw.Ingredient_Raw = recipeRaw.Ingredient_Raw;
                }
                if (recipeRaw.Quantity != null)
                {
                    existingRecipeRaw.Quantity = recipeRaw.Quantity;
                }
                if (recipeRaw.Unit != null)
                {
                    existingRecipeRaw.Unit = recipeRaw.Unit;
                }
                if (recipeRaw.IsActive != null)
                {
                    existingRecipeRaw.IsActive = recipeRaw.IsActive;
                }

                existingRecipeRaw.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }
    }
}
