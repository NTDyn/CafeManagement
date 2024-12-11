using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Warehouse>> GetWarehouse(Nullable<int> warehouseID , Nullable<bool> isActive)
        {
            List<Warehouse> warehouseList = null;
            Expression<Func<Warehouse, bool>> _Filter = r => true;

            if (warehouseID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.WareHouse_ID == warehouseID);
            }

            if (isActive != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.IsActive == isActive);
            }

            warehouseList = await _context.Warehouse.Where(_Filter).ToListAsync();

            return warehouseList;
        }

        public async Task AddWarehouse(Warehouse warehouse)
        {
            warehouse.CreatedDate = DateTime.Now;
            warehouse.ModifiedDate = DateTime.Now;

            await _context.Warehouse.AddAsync(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWarehouse(Warehouse warehouse)
        {
            var existingWarehouse = await _context.Warehouse.FindAsync(warehouse.WareHouse_ID);
            if (existingWarehouse != null)
            {
                if (warehouse.WareHouse_Name != null)
                {
                    existingWarehouse.WareHouse_Name = warehouse.WareHouse_Name;
                }
                if (warehouse.IsActive != null)
                {
                    existingWarehouse.IsActive = warehouse.IsActive;
                }

                existingWarehouse.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Warehouse>> GetWareHouseActive()
        {
           return await _context.Warehouse.Where(w=>w.IsActive==true).ToListAsync();
        }

        public async Task<Warehouse> getWareHouseById(int id)
        {
            return await _context.Warehouse.FindAsync(id);
        }
    }
}
