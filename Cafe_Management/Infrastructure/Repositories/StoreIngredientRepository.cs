using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class StoreIngredientRepository : IStoreIngredientRepository
    {
        private readonly AppDbContext _context;
        public StoreIngredientRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StoreIngredient>> Get(Nullable<int> Ingredient_ID = null, Nullable<int> Warehouse_ID = null)
        {
            List<StoreIngredient> Data = null;
            Expression<Func<StoreIngredient, bool>> _Filter = r => true;

            if (Ingredient_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Ingredient_ID == Ingredient_ID);
            }

            if (Warehouse_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Warehouse_ID == Warehouse_ID);
            }
           
            Data = await _context.StoreIngredient.Where(_Filter).ToListAsync();

            return Data;
        }


        public async Task Create(StoreIngredient StoreIngredient)
        {
            StoreIngredient.CreatedDate = DateTime.Now;
            StoreIngredient.ModifiedDate = DateTime.Now;

            await _context.StoreIngredient.AddAsync(StoreIngredient);
            await _context.SaveChangesAsync();
        }
        public async Task Update(StoreIngredient StoreIngredient)
        {
            var existing = await _context.StoreIngredient.SingleOrDefaultAsync(x=>x.Ingredient_ID == StoreIngredient.Ingredient_ID && x.Warehouse_ID == StoreIngredient.Warehouse_ID);
            if (existing != null)
            {
                if(StoreIngredient.Quality != null)
                {
                    existing.Quality = StoreIngredient.Quality;
                }
                if (StoreIngredient.Price != null)
                {
                    existing.Price = StoreIngredient.Price;
                }

                existing.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
