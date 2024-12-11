using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Infrastructure.Data;
using System.Collections.Generic;
using System.Data.Odbc;

namespace Cafe_Management.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync(Nullable<bool> isActive);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
    }
}
