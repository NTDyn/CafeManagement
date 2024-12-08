using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly AppDbContext _context;
        public StaffRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Staff>> Get(
                                                Nullable<int> Staff_ID = null, 
                                                Nullable<bool> IsActive = null,
                                                Nullable<int> StaffGroup_ID = null,
                                                string Username = null,
                                                string Password = null,
                                                string Staff_Phone = null)
        {
            List<Staff> Data = null;
            Expression<Func<Staff, bool>> _Filter = r => true;

            if (Staff_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Staff_ID == Staff_ID);
            }

            if (IsActive != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.IsActive == IsActive);
            }
            if (StaffGroup_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.StaffGroup_ID == StaffGroup_ID);
            }
            if (Username != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Username == Username);
            }
            if (Password != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Password == Password);
            }
            if (Staff_Phone != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Staff_Phone == Staff_Phone);
            }
            Data = await _context.Staff.Where(_Filter).ToListAsync();

            return Data;
        }


        public async Task Create(Staff staff)
        {
            staff.CreatedDate = DateTime.Now;
            staff.ModifiedDate = DateTime.Now;

            await _context.Staff.AddAsync(staff);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Staff staff)
        {
            var existing = await _context.Staff.FindAsync(staff.Staff_ID);
            if (existing != null)
            {
                if (staff.Staff_FullName != null)
                {
                    existing.Staff_FullName = staff.Staff_FullName;
                }
                if (staff.IsActive != existing.IsActive)
                {
                    existing.IsActive = staff.IsActive;
                }
                if(staff.StaffGroup_ID != existing.StaffGroup_ID)
                {
                    existing.StaffGroup_ID = staff.StaffGroup_ID;
                }
                if (staff.Staff_Phone != existing.Staff_Phone) 
                { 
                    existing.Staff_Phone = staff.Staff_Phone;
                }
                if(staff.Password != null)
                {
                    existing.Password = staff.Password;
                }

                existing.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }
    }
}
