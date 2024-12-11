using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class BatchRecipeRepository : IBatchRecipeRepository
    {
        private readonly AppDbContext _context;
        public BatchRecipeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BatchRecipe>> Get(Nullable<int> BatchRecipe_ID = null)
        {
            Expression<Func<BatchRecipe, bool>> _Filter = r => true;

            if (BatchRecipe_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.BatchRecipe_ID == BatchRecipe_ID);
            }
            List<BatchRecipe> BatchRecipe = await _context.BatchRecipe.Where(_Filter).ToListAsync();
            List<BatchRecipeDetail> BatchRecipeDetail = await _context.BatchRecipeDetail.Where(x => x.IsActive == true).ToListAsync();
            var JoinData = (from h in BatchRecipe
                            join d in BatchRecipeDetail
                            on h.BatchRecipe_ID equals d.BatchRecipe_ID
                            into groups
                            select new BatchRecipe
                            {
                                BatchRecipe_ID = h.BatchRecipe_ID,
                                Staff_ID = h.Staff_ID,
                                IngredientResult_ID = h.IngredientResult_ID,
                                Quality = h.Quality,
                                Unit = h.Unit,
                                IsActive = h.IsActive,
                                CreatedDate = h.CreatedDate,
                                ModifiedDate = h.ModifiedDate,
                                Details = groups.ToList()
                            }).ToList();
            return JoinData;
        }

        public async Task Create(BatchRecipe BatchRecipe)
        {
            BatchRecipe.CreatedDate = DateTime.Now;
            BatchRecipe.ModifiedDate = DateTime.Now;
            var maxId = await _context.BatchRecipe.MaxAsync(p => (int?)p.BatchRecipe_ID) ?? 0;

            int ID = maxId + 1;
            BatchRecipe.IsActive = true;
            BatchRecipe.CreatedDate = DateTime.Now;
            BatchRecipe.ModifiedDate = DateTime.Now;
            Ingredient? ingredientBatch = await _context.Ingredient.SingleOrDefaultAsync(x => x.Ingredient_ID == BatchRecipe.IngredientResult_ID && x.Ingredient_Type == 1);
            double TotalQuantity = (double)(BatchRecipe.Unit == 2 ? (BatchRecipe.Quality * ingredientBatch.MaxPerTransfer * ingredientBatch.TransferPerMin) : BatchRecipe.Unit == 1 ? (BatchRecipe.Quality * ingredientBatch.TransferPerMin) : BatchRecipe.Quality); ;

            StoreIngredient? storeIngredient = await _context.StoreIngredient.Where(x => x.Ingredient_ID == BatchRecipe.IngredientResult_ID).SingleOrDefaultAsync();
            if (storeIngredient != null)
            {
                //Cong KHO
                double Quan = (double)storeIngredient.Quality + TotalQuantity;
                storeIngredient.Quality = Quan;
            }
            else
            {
                StoreIngredient addBat = new StoreIngredient();
                addBat.Warehouse_ID = 0;
                addBat.Ingredient_ID = BatchRecipe.IngredientResult_ID;
                addBat.Price = 0;
                addBat.Quality = TotalQuantity;
                addBat.IsActive = true;
                addBat.CreatedDate = DateTime.Now;
                addBat.ModifiedDate = DateTime.Now;
                await _context.StoreIngredient.AddAsync(addBat);
            }
            List<RecipeRaw> recipeRaws = await _context.RecipeRaw.Where(x => x.Ingredient_Result == BatchRecipe.IngredientResult_ID).ToListAsync();
            if (recipeRaws != null && recipeRaws.Count > 0)
            {
                foreach (var recipe in recipeRaws)
                {
                    BatchRecipeDetail batchRecipeDetail = new BatchRecipeDetail();

                    Ingredient? ingredientRaw = await _context.Ingredient.SingleOrDefaultAsync(x => x.Ingredient_ID == recipe.Ingredient_Raw);
                    double Quantity = TotalQuantity * recipe.Quantity;
                    batchRecipeDetail.Unit = 0;
                    batchRecipeDetail.BatchRecipe_ID = ID;
                    batchRecipeDetail.Quality = Quantity;
                    batchRecipeDetail.Ingredient_ID = recipe.Ingredient_Raw;
                    batchRecipeDetail.IsActive = true;
                    batchRecipeDetail.CreatedDate = DateTime.Now;
                    batchRecipeDetail.ModifiedDate = DateTime.Now;
                    StoreIngredient? storeIngredientRaw = await _context.StoreIngredient.Where(x => x.Ingredient_ID == recipe.Ingredient_Raw).SingleOrDefaultAsync();
                    if (storeIngredient != null)
                    {
                        //Tru kho
                        double Quan = (double)storeIngredient.Quality - Quantity;
                        storeIngredientRaw.Quality = Quan;
                    }
                    else
                    {
                        StoreIngredient add = new StoreIngredient();
                        add.Warehouse_ID = 0;
                        add.Ingredient_ID = recipe.Ingredient_Raw;
                        add.Price = 0;
                        add.Quality = Quantity;
                        add.IsActive = true;
                        add.CreatedDate = DateTime.Now;
                        add.ModifiedDate = DateTime.Now;
                        await _context.StoreIngredient.AddAsync(add);
                    }
                    await _context.BatchRecipeDetail.AddAsync(batchRecipeDetail);
                }
            }
            await _context.BatchRecipe.AddAsync(BatchRecipe);
            await _context.SaveChangesAsync();
        }
    }
}
