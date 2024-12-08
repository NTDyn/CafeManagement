using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class StaffGroupLinkPermissionRepository : IStaffGroupPermissionResponsitory
    {
        private readonly AppDbContext _context;
        public StaffGroupLinkPermissionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StaffGroupLinkPermission>> Get(
                                                Nullable<int> Permission_ID,
                                                Nullable<bool> IsActive,
                                                Nullable<int> StaffGroup)
        {
            List<StaffGroupLinkPermission> Data = null;
            Expression<Func<StaffGroupLinkPermission, bool>> _Filter = r => true;

            if (Permission_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Permission_ID == Permission_ID);
            }

            if (IsActive != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.IsActive == IsActive);
            }
            if (StaffGroup != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.StaffGroup == StaffGroup);
            }
           
            Data = await _context.StaffGroupLinkPermission.Where(_Filter).ToListAsync();
            return Data;
        }


        public async Task Create(StaffGroupLinkPermission staffGroupLinkPermission)
        {
            staffGroupLinkPermission.CreatedDate = DateTime.Now;
            staffGroupLinkPermission.ModifiedDate = DateTime.Now;

            await _context.StaffGroupLinkPermission.AddAsync(staffGroupLinkPermission);
            await _context.SaveChangesAsync();
        }
        public async Task Update(StaffGroupLinkPermission staffGroupLinkPermission)
        {
            var existing = await _context.StaffGroupLinkPermission.SingleOrDefaultAsync(x=> x.StaffGroup == staffGroupLinkPermission.StaffGroup && x.Permission_ID == staffGroupLinkPermission.Permission_ID);
            if (existing != null)
            {
               if(staffGroupLinkPermission.IsActive != null)
                {
                    existing.IsActive = true;
                }

                existing.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }
    }
}
