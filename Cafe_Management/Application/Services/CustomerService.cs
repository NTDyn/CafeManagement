using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetCustomers(Nullable<int> Customer_ID, Nullable<bool> IsActive, Nullable<int> Level_ID)
        {
            return await _customerRepository.GetCustomers(Customer_ID, IsActive, Level_ID);
        }

        public async Task AddCustomer(Customer customer)
        {
            await _customerRepository.AddCustomer(customer);
        }



        public async Task UpdateCustomer(Customer customer)
        {
            await _customerRepository.UpdateCustomer(customer);
        }
        public async Task<Customer> getCustomerByUserName(string userName)
        {
            return await _customerRepository.getCustomerByUserName(userName);
        }
    }
}
