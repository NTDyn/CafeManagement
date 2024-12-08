using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class StaffGroupRepository:IStaffGroupRepository
    {
        private readonly AppDbContext _context;
        private readonly StaffGroupPermissionLinkService _permission;
        public StaffGroupRepository(AppDbContext context, StaffGroupPermissionLinkService permission)
        {
            _context = context;
            _permission = permission;
        }
        public async Task<IEnumerable<StaffGroup>> Get(
                                                Nullable<int> StaffGroup_ID,
                                                Nullable<bool> IsActive)
        {
            List<StaffGroup> Data = null;
            Expression<Func<StaffGroup, bool>> _Filter = r => true;

            if (StaffGroup_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.StaffGroup_ID == StaffGroup_ID);
            }

            if (IsActive != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.IsActive == IsActive);
            }
            List<StaffGroup> staffGroups = await _context.StaffGroup.Where(_Filter).ToListAsync();
            List<StaffGroupLinkPermission> staffGroupLinkPermissions = await _context.StaffGroupLinkPermission.Where(x=>x.IsActive == true).ToListAsync();
            var JoinData = (from sg in staffGroups
                                  join sgp in staffGroupLinkPermissions
                                  on sg.StaffGroup_ID equals sgp.StaffGroup
                                  into groupPermissions
                                  select new StaffGroup
                                  {
                                      StaffGroup_ID = sg.StaffGroup_ID,
                                      StaffGroup_Name = sg.StaffGroup_Name,
                                      IsActive = sg.IsActive,
                                      CreatedDate = sg.CreatedDate,
                                      ModifiedDate = sg.ModifiedDate,
                                      Permissions = groupPermissions.ToList()
                                  }).ToList();

            return JoinData;
        }


        public async Task Create(StaffGroup staffGroup)
        {
            staffGroup.CreatedDate = DateTime.Now;
            staffGroup.ModifiedDate = DateTime.Now;
            var maxId = await _context.StaffGroup.MaxAsync(p => (int?)p.StaffGroup_ID) ?? 0;

            int id = maxId + 1;
            if (staffGroup.Permissions != null && staffGroup.Permissions.Count > 0)
            {
                foreach(var permission in staffGroup.Permissions)
                {
                    StaffGroupLinkPermission staffGroupLinkPermission = new StaffGroupLinkPermission();
                    staffGroupLinkPermission.StaffGroup = id;
                    staffGroupLinkPermission.Permission_ID = permission.Permission_ID;
                    await _permission.Create(staffGroupLinkPermission);
                }
            }
            await _context.StaffGroup.AddAsync(staffGroup);
            await _context.SaveChangesAsync();
        }
        public async Task Update(StaffGroup staffGroup)
        {
            var existing = await _context.StaffGroup.FindAsync(staffGroup.StaffGroup_ID);
            if (existing != null)
            {
                if (staffGroup.StaffGroup_Name != null)
                {
                    existing.StaffGroup_Name = staffGroup.StaffGroup_Name;
                }
                if (staffGroup.IsActive != existing.IsActive)
                {
                    existing.IsActive = staffGroup.IsActive;
                }
                if(staffGroup.Permissions != null && staffGroup.Permissions.Count > 0)
                {
                    var current = _permission.Get(null,null,staffGroup.StaffGroup_ID).Result;

                    foreach (var permissions in staffGroup.Permissions)
                    {
                        bool exists = current.Any(r => r.Permission_ID == permissions.Permission_ID);
                        StaffGroupLinkPermission staffGroupLinkPermission = new StaffGroupLinkPermission();
                        staffGroupLinkPermission.StaffGroup = (int)staffGroup.StaffGroup_ID;
                        staffGroupLinkPermission.Permission_ID = permissions.Permission_ID;
                        if (exists == true)
                        {
                            //await _permission.Update(staffGroupLinkPermission);
                        }
                        else
                        {
                            await _permission.Create(staffGroupLinkPermission);
                        }

                    }
                    //DELETE
                    var deleteProductRecipe = staffGroup.Permissions.Where(itemA => !current.Any(itemB => itemB.Permission_ID == itemA.Permission_ID)).ToList();
                    foreach (var permission in deleteProductRecipe)
                    {
                        StaffGroupLinkPermission staffGroupLinkPermission = new StaffGroupLinkPermission();
                        staffGroupLinkPermission.StaffGroup = (int)staffGroup.StaffGroup_ID;
                        staffGroupLinkPermission.Permission_ID = permission.Permission_ID;
                        staffGroupLinkPermission.IsActive = false;
                        await _permission.Update(staffGroupLinkPermission);
                    }
                }

                existing.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }
    }
}
