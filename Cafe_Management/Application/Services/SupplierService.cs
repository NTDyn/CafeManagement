using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class SupplierService 
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            return await _supplierRepository.GetAllSupplier();
        }

        public async Task AddSupplier(Supplier? supplier)
        {
          
             await _supplierRepository.AddSupplier(supplier);
        }

        public async  Task UpdateSupplier(Supplier supplier)
        {
           await _supplierRepository .UpdateSupplier(supplier);    
        }
        public async Task<Supplier?>getOneSupplier(int id)
        {
            return await _supplierRepository.GetSupplierById(id);

        }
        public async Task<bool>checkNameSupplier(Supplier supplier)
        {
            return await _supplierRepository.checkExistNameForUpdate(supplier);
        }
    }
}
