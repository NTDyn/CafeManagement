using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class ReceiptRepository: IReceiptRepository
    {
        private readonly AppDbContext _context;
        public ReceiptRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Receipt>> Get(Nullable<int> Receipt_ID = null)
        {
            Expression<Func<Receipt, bool>> _Filter = r => true;

            if (Receipt_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Receipt_ID == Receipt_ID);
            }
            List<Receipt> Receipt = await _context.Receipt.Where(_Filter).ToListAsync();
            List<ReceiptDetail> ReceiptDetail = await _context.ReceiptDetail.Where(x => x.IsActive == true).ToListAsync();
            var JoinData = (from h in Receipt
                            join d in ReceiptDetail
                            on h.Receipt_ID equals d.Receipt_ID
                            into groups
                            select new Receipt
                            {
                                Receipt_ID = h.Receipt_ID,
                                Staff_ID = h.Staff_ID,
                                IsActive = h.IsActive,
                                TotalPrice = h.TotalPrice,
                                CreatedDate = h.CreatedDate,
                                ModifiedDate = h.ModifiedDate,
                                Details = groups.ToList()
                            }).ToList();
            return JoinData;
        }

        public async Task Create(Receipt Receipt)
        {
            Receipt.CreatedDate = DateTime.Now;
            Receipt.ModifiedDate = DateTime.Now;
            int TotalPrice = 0;
            var maxId = await _context.Receipt.MaxAsync(p => (int?)p.Receipt_ID) ?? 0;

            int ID = maxId + 1;
            if (Receipt.Details != null &&  Receipt.Details.Count > 0)
            {
                
                foreach (var d in Receipt.Details) {
                    if(d.Quantity > 0)
                    {
                        TotalPrice += d.Quantity * d.Price;
                        d.Receipt_ID = ID;
                        await _context.ReceiptDetail.AddAsync(d);
                        List<ProductRecipe> ProductRecipes = await _context.ProductRecipe.Where(x => x.Product_ID == d.Product_ID).ToListAsync();
                        foreach (var recipe in ProductRecipes) 
                        {

                            double TotalRecipe = 0;
                            Ingredient? ingredient = await _context.Ingredient.FindAsync(recipe.Ingredient_ID);
                            if (ingredient != null)
                            {
                                TotalRecipe = (double)(recipe.Unit == 2 ? (recipe.Unit * ingredient.MaxPerTransfer * ingredient.TransferPerMin) : recipe.Unit == 1 ? (recipe.Quantity * ingredient.TransferPerMin) : recipe.Quantity);
                            }
                            StoreIngredient? storeIngredient = await _context.StoreIngredient.Where(x => x.Ingredient_ID == recipe.Ingredient_ID).SingleOrDefaultAsync();
                            if (storeIngredient != null) 
                            {
                                //TRU KHO
                                double Quan = (double)storeIngredient.Quality - TotalRecipe;
                                storeIngredient.Quality = Quan;
                            }
                            else
                            {
                                StoreIngredient add = new StoreIngredient();
                                add.Warehouse_ID = 0;
                                add.Ingredient_ID = recipe.Ingredient_ID;
                                add.Price = 0;
                                add.Quality = TotalRecipe;
                                await _context.StoreIngredient.AddAsync(storeIngredient);
                            }
                        }
                    }
                    
                }
            }
            Receipt.TotalPrice = TotalPrice;
            await _context.Receipt.AddAsync(Receipt);
            await _context.SaveChangesAsync();
        }
        //public async Task Update(Receipt Receipt)
        //{
        //    var existing = await _context.Receipt.SingleOrDefaultAsync(x => x.Receipt_ID == Receipt.Receipt_ID);
        //    if (existing != null)
        //    {
                

        //        existing.ModifiedDate = DateTime.Now;
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
