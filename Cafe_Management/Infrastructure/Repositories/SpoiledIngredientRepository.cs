using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class SpoiledIngredientRepository: ISpoiledIngredientRepository
    {
        private readonly AppDbContext _context;
        public SpoiledIngredientRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SpoiledIngredient>> Get(Nullable<int> Spoiled_ID = null)
        {
            Expression<Func<SpoiledIngredient, bool>> _Filter = r => true;

            if (Spoiled_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Spoiled_ID == Spoiled_ID);
            }
            List<SpoiledIngredient> SpoiledIngredient = await _context.SpoiledIngredient.Where(_Filter).ToListAsync();
            List<SpoiledIngredientDetail> SpoiledIngredientDetail = await _context.SpoiledIngredientDetail.Where(x => x.IsActive == true).ToListAsync();
            var JoinData = (from h in SpoiledIngredient
                            join d in SpoiledIngredientDetail
                            on h.Spoiled_ID equals d.Spoiled_ID
                            into groups
                            select new SpoiledIngredient
                            {
                                Spoiled_ID = h.Spoiled_ID,
                                Staff_ID = h.Staff_ID,
                                IsActive = h.IsActive,
                                Reason = h.Reason,
                                CreatedDate = h.CreatedDate,
                                ModifiedDate = h.ModifiedDate,
                                Details = groups.ToList()
                            }).ToList();
            return JoinData;
        }

        public async Task Create(SpoiledIngredient SpoiledIngredient)
        {
            SpoiledIngredient.CreatedDate = DateTime.Now;
            SpoiledIngredient.ModifiedDate = DateTime.Now;
            var maxId = await _context.SpoiledIngredient.MaxAsync(p => (int?)p.Spoiled_ID) ?? 0;

            int ID = maxId + 1;
            
            if (SpoiledIngredient.Details != null && SpoiledIngredient.Details.Count > 0)
            {

                foreach (var d in SpoiledIngredient.Details)
                {
                    if (d.Quality > 0)
                    {
                        d.Spoiled_ID = ID;
                        await _context.SpoiledIngredientDetail.AddAsync(d);

                        Ingredient? ingredient = await _context.Ingredient.FindAsync(d.Ingredient_ID);
                        double TotalQuantity = 0;
                        if (ingredient != null)
                        {
                            TotalQuantity = (double)(d.Unit == 2 ? (d.Quality * ingredient.MaxPerTransfer * ingredient.TransferPerMin) : d.Unit == 1 ? (d.Quality * ingredient.TransferPerMin) : d.Quality);
                        }
                        StoreIngredient? storeIngredient = await _context.StoreIngredient.Where(x => x.Ingredient_ID == d.Ingredient_ID).SingleOrDefaultAsync();
                        if (storeIngredient != null)
                        {
                            //TRU KHO
                            double Quan = (double)storeIngredient.Quality - TotalQuantity;
                            storeIngredient.Quality = Quan;
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
            await _context.SpoiledIngredient.AddAsync(SpoiledIngredient);
            await _context.SaveChangesAsync();
        }
    }
}
