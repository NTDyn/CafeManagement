using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class CustomerLevelRepository : ICustomerLevelRepository
    {
        private readonly AppDbContext _context;
        public CustomerLevelRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CustomerLevel>> GetCustomerLevels(Nullable<int> Level_ID, Nullable<bool> IsActive)
        {
            List<CustomerLevel> levelList = null;
            Expression<Func<CustomerLevel, bool>> _Filter = r => true;

            if (Level_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Level_ID == Level_ID);
            }

            if (IsActive != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.IsActive == IsActive);
            }

            levelList = await _context.CustomerLevel.Where(_Filter).ToListAsync();

            return levelList;
        }


        public async Task AddCustomerLevel(CustomerLevel customerLevel)
        {
            customerLevel.CreatedDate = DateTime.Now;
            customerLevel.ModifiedDate = DateTime.Now;
            customerLevel.IsActive = true;

            await _context.CustomerLevel.AddAsync(customerLevel);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCustomerLevel(CustomerLevel customerLevel)
        {
            var existingLevel = await _context.CustomerLevel.FindAsync(customerLevel.Level_ID);
            if (existingLevel != null)
            {
                if (customerLevel.Level_Name != null)
                {
                    existingLevel.Level_Name = customerLevel.Level_Name;
                }
                if (customerLevel.PointApply != null)
                {
                    existingLevel.PointApply = customerLevel.PointApply;
                }
                if (customerLevel.IsActive != null)
                {
                    existingLevel.IsActive = customerLevel.IsActive;
                }

                existingLevel.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }
    }
}
