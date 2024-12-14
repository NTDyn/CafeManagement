using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class HistoryDiscountRepository: IHistoryDisccountRepository
    {
        private readonly AppDbContext _context;
        public HistoryDiscountRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HistoryDisscount>> GetHistoryDiscounts(Nullable<int>History_ID, Nullable<int> Customer_ID)
        {
            

            if (History_ID != null)
            {
                var discountList = await _context.HistoryDisscount.Where(h => h.History_ID == History_ID).ToListAsync();
                return discountList;
            }

              var joinData =  await (from hd in _context.HistoryDisscount.Where(h => h.Customer_ID == Customer_ID && h.IsActive == true)
                                      join rec in _context.Receipt.Where(r => r.IsActive == true)
                                      on hd.Receipt_ID equals rec.Receipt_ID
                                      select new HistoryDisscount
                                      {
                                          History_ID = hd.History_ID,
                                          Customer_ID = hd.Customer_ID,
                                          Cuppon_ID = hd.Cuppon_ID,
                                          PriceDisscount = hd.PriceDisscount,
                                          Receipt_ID = hd.Receipt_ID,
                                          IsActive = hd.IsActive,
                                          CreatedDate = hd.CreatedDate,
                                          ModifiedDate = hd.ModifiedDate

                                      }).ToListAsync();

            return joinData;
        }


        public async Task AddHistoryDiscount(HistoryDisscount historyDisscount)
        {
            historyDisscount.CreatedDate = DateTime.Now;
            historyDisscount.ModifiedDate = DateTime.Now;
            historyDisscount.IsActive = true;

            await _context.HistoryDisscount.AddAsync(historyDisscount);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateHistoryDiscount(HistoryDisscount historyDisscount)
        {
            var existingHistoryDiscount = await _context.HistoryDisscount.FindAsync(historyDisscount.History_ID);
            if (existingHistoryDiscount != null)
            {
                if (historyDisscount.Cuppon_ID != null)
                {
                    existingHistoryDiscount.Cuppon_ID = historyDisscount.Cuppon_ID;
                }
                if (historyDisscount.PriceDisscount != null)
                {
                    existingHistoryDiscount.PriceDisscount = historyDisscount.PriceDisscount;
                }
                if (historyDisscount.IsActive != null)
                {
                    existingHistoryDiscount.IsActive = historyDisscount.IsActive;
                }


                existingHistoryDiscount.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }
    }
}
