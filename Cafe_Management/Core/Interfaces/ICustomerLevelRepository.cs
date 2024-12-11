using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface ICustomerLevelRepository
    {
        Task<IEnumerable<CustomerLevel>> GetCustomerLevels(Nullable<int> Level_ID, Nullable<bool> IsActive);
        Task AddCustomerLevel(CustomerLevel customerLevel);
        Task UpdateCustomerLevel(CustomerLevel customerLevel);
    }
}
