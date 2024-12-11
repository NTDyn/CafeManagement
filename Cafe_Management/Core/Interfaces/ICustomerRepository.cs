using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers(Nullable<int> Customer_ID,
                                               Nullable<bool> IsActive,
                                               Nullable<int> Level_ID);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
    }
}
