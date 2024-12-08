using Cafe_Management.Code;
using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface ISupplierRepository
    {
        Task<Supplier> GetSupplierById(int id);
        Task<IEnumerable<Supplier>> GetAllSupplier();
        Task AddSupplier(Supplier supplier);
        Task UpdateSupplier(Supplier supplier);
        Task<bool> checkExistNameForUpdate(Supplier supplier);
    }
}
