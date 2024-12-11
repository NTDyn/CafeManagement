using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class WarehouseService
    {

        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<Warehouse>> GetWarehouse(Nullable<int> Warehouse_ID, Nullable<bool> IsActive)
        {
            return await _warehouseRepository.GetWarehouse(Warehouse_ID, IsActive);
        }

        public async Task AddWarehouse(Warehouse warehouse)
        {
            await _warehouseRepository.AddWarehouse(warehouse);
        }

        public async Task UpdateWarehouse(Warehouse warehouse)
        {
            await _warehouseRepository.UpdateWarehouse(warehouse);
        }
        public async Task<IEnumerable<Warehouse>> GetWareHouseActive()
        {
           return  await _warehouseRepository.GetWareHouseActive();
        }

        public async Task<Warehouse>getWareHouseById(int id)
        {
            return await _warehouseRepository.getWareHouseById(id);
        }
    }
}
