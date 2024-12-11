using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly AppDbContext _context;

        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ingredient?> GetIngredientById(int id)
        {
            return await _context.Ingredient.FindAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredients(Nullable<int> Type = null)
        {
            List<Ingredient> ingredients = null;
            if (Type != null)
            {
                ingredients = await _context.Ingredient.Where(x => x.Ingredient_Type == Type).ToListAsync();
            }
            ingredients = await _context.Ingredient.ToListAsync();
            List<RecipeRaw> RecipeRaws = await _context.RecipeRaw.ToListAsync();

            var JoinData = (from d in ingredients
                            join r in RecipeRaws on d.Ingredient_ID equals r.Ingredient_Result
                            into groupjoin
                            select new Ingredient
                            {
                                Ingredient_ID = d.Ingredient_ID,
                                Ingredient_Name = d.Ingredient_Name,
                                Ingredient_Category = d.Ingredient_Category,
                                Ingredient_Type = d.Ingredient_Type,
                                Unit_Transfer = d.Unit_Transfer,
                                Unit_Min = d.Unit_Min,
                                Unit_Max = d.Unit_Max,
                                TransferPerMin = d.TransferPerMin,
                                MaxPerTransfer = d.MaxPerTransfer,
                                IsActive = d.IsActive,
                                CreatedDate = d.CreatedDate,
                                ModifiedDate = d.ModifiedDate,
                                RecipeRaws = groupjoin.ToList()
                            }).ToList();
            return JoinData;
        }

        public async Task AddIngredient(Ingredient ingredient)
        {
            ingredient.CreatedDate = DateTime.Now;
            ingredient.ModifiedDate = DateTime.Now;

            // Tìm giá trị ProductID lớn nhất hiện tại
            var maxId = await _context.Ingredient.MaxAsync(p => (int?)p.Ingredient_ID) ?? 0;

            // Tự động tăng ID cho sản phẩm mới
            ingredient.Ingredient_ID = maxId + 1;
            await _context.Ingredient.AddAsync(ingredient);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateIngredient(Ingredient ingredient)
        {
            var existingIngredient = await _context.Ingredient.FindAsync(ingredient.Ingredient_ID);
            if (existingIngredient != null)
            {
                if (ingredient.IsActive != null)
                {
                    existingIngredient.IsActive = ingredient.IsActive;
                    await _context.SaveChangesAsync(); // Lưu thay đổi vào database
                    return;
                }
                if (ingredient.IsActive == null)
                {
                    existingIngredient.Ingredient_Name = ingredient.Ingredient_Name;
                    existingIngredient.Ingredient_Category = ingredient.Ingredient_Category;
                    await _context.SaveChangesAsync(); // Lưu thay đổi vào database
                }

            }
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientActive()
        {
            return await _context.Ingredient.Where(i =>i.IsActive==true).ToListAsync();
        }
    }
}
