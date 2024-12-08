using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Newtonsoft.Json;
using System.Data.Odbc;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Cafe_Management.Infrastructure.Data;
using System.Diagnostics.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management.Infrastructure.Repositories
{


    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _dbContext;
        public SupplierRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddSupplier(Supplier? supplier)
        {
            supplier.CreatedDate = DateTime.Now;
            supplier.ModifiedDate = DateTime.Now;
            supplier.IsActive = true;
            await _dbContext.Supplier.AddAsync(supplier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> checkExistNameForUpdate(Supplier supplier)
        {
            var check  = await _dbContext.Supplier.FirstOrDefaultAsync(s=>(s.Supplier_Name==supplier.Supplier_Name && s.Supplier_ID != supplier.Supplier_ID));
            return check  != null;

        }

        public async Task<IEnumerable<Supplier>> GetAllSupplier()
        {
            return await _dbContext.Supplier.ToListAsync();
        }

        public async Task<Supplier?> GetSupplierById(int id)
        {
            return await _dbContext.Supplier.FindAsync(id);

                
        }

        public async Task UpdateSupplier(Supplier supplier)
        {
            var checkSupplier = await _dbContext.Supplier.FirstOrDefaultAsync(s => s.Supplier_ID == supplier.Supplier_ID);
            if (checkSupplier != null)
            {
                checkSupplier.Supplier_Name = supplier.Supplier_Name;
                checkSupplier.ModifiedDate = DateTime.Now;
                checkSupplier.IsActive = supplier.IsActive;
                await _dbContext.SaveChangesAsync();
            }
           
        }
    }


}
