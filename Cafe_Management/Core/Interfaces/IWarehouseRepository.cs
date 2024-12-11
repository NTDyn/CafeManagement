using Cafe_Management.Code;
using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<IEnumerable<Warehouse>> GetWarehouse(Nullable<int> warehouseID , Nullable<bool> isActive);

        Task AddWarehouse(Warehouse warehouse);
        Task UpdateWarehouse(Warehouse warehouse);
        Task<IEnumerable<Warehouse>> GetWareHouseActive();
        Task<Warehouse> getWareHouseById(int id);
    }
}
