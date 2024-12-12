using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class CupponRepository : ICupponRepository
    {
        private readonly AppDbContext _context;
        public CupponRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cuppon>> GetCuppons(Nullable<int> Cuppon_ID, Nullable<bool> IsActive)
        {
            List<Cuppon> CupponList = null;
            Expression<Func<Cuppon, bool>> _Filter = r => true;

            if (Cuppon_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Cuppon_ID == Cuppon_ID);
            }

            if (IsActive != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.IsActive == IsActive);
            }

           

            CupponList = await _context.Cuppon.Where(_Filter).ToListAsync();

            return CupponList;
        }


        public async Task AddCuppon(Cuppon cuppon)
        {
            cuppon.CreatedDate = DateTime.Now;
            cuppon.ModifiedDate = DateTime.Now;

            await _context.Cuppon.AddAsync(cuppon);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCuppon(Cuppon cuppon)
        {
            var existingCuppon = await _context.Cuppon.FindAsync(cuppon.Cuppon_ID);
            if (existingCuppon != null)
            {
                if (cuppon.Cuppon_Name != null)
                {
                    existingCuppon.Cuppon_Name = cuppon.Cuppon_Name;
                }
                if (cuppon.Disscount != null)
                {
                    existingCuppon.Disscount = cuppon.Disscount;
                }
                if (cuppon.Cuppon_Type != null)
                {
                    existingCuppon.Cuppon_Type = cuppon.Cuppon_Type;
                }
                if (cuppon.ApplyLevel_ID != null)
                {
                    existingCuppon.ApplyLevel_ID = cuppon.ApplyLevel_ID;
                }
                if (cuppon.DateStart != null)
                {
                    existingCuppon.DateStart = cuppon.DateStart;
                }
                if (cuppon.DateEnd != null)
                {
                    existingCuppon.DateEnd = cuppon.DateEnd;
                }
                if (cuppon.IsActive != null)
                {
                    existingCuppon.IsActive = cuppon.IsActive;
                }

                existingCuppon.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }
    }
}
