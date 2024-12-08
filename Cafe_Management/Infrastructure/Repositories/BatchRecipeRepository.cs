using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class BatchRecipeRepository:IBatchRecipeRepository
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

            if (BatchRecipe.Details != null && BatchRecipe.Details.Count > 0)
            {

                foreach (var d in BatchRecipe.Details)
                {
                    if (d.Quality > 0)
                    {
                        d.BatchRecipe_ID = ID;
                        await _context.BatchRecipeDetail.AddAsync(d);

                        Ingredient? ingredientBatch = await _context.Ingredient.SingleOrDefaultAsync(x=>x.Ingredient_ID == d.Ingredient_ID && x.Ingredient_Type == 1);
                        double TotalQuantity = 0;
                        if (ingredientBatch != null)
                        {
                            TotalQuantity = (double)(d.Unit == 2 ? (d.Quality * ingredientBatch.MaxPerTransfer * ingredientBatch.TransferPerMin) : d.Unit == 1 ? (d.Quality * ingredientBatch.TransferPerMin) : d.Quality);
                        }
                        StoreIngredient? storeIngredient = await _context.StoreIngredient.Where(x => x.Ingredient_ID == d.Ingredient_ID).SingleOrDefaultAsync();
                        if (storeIngredient != null)
                        {
                            //Cong KHO
                            double Quan = (double)storeIngredient.Quality + TotalQuantity;
                            storeIngredient.Quality = Quan;
                            //TRU KHO ?
                        }
                        else
                        {
                            StoreIngredient add = new StoreIngredient();
                            add.Warehouse_ID = 0;
                            add.Ingredient_ID = d.Ingredient_ID;
                            add.Price = 0;
                            add.Quality = TotalQuantity;
                            await _context.StoreIngredient.AddAsync(storeIngredient);
                        }
                    }

                }
            }
            await _context.BatchRecipe.AddAsync(BatchRecipe);
            await _context.SaveChangesAsync();
        }
    }
}
