using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class CustomerLevelService
    {
        private readonly ICustomerLevelRepository _levelRepository;

        public CustomerLevelService(ICustomerLevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<IEnumerable<CustomerLevel>> GetCustomerLevels(Nullable<int> Level_ID, Nullable<bool> IsActive)
        {
            return await _levelRepository.GetCustomerLevels(Level_ID, IsActive);
        }

        public async Task AddCustomerLevel(CustomerLevel customerLevel)
        {
            await _levelRepository.AddCustomerLevel(customerLevel);
        }



        public async Task UpdateCustomerLevel(CustomerLevel customerLevel)
        {
            await _levelRepository.UpdateCustomerLevel(customerLevel);
        }
    }
}
