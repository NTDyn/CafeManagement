using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class StaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<IEnumerable<Staff>> Get(Nullable<int> Staff_ID,
                                                Nullable<bool> IsActive,
                                                Nullable<int> StaffGroup_ID,
                                                string Username,
                                                string Password,
                                                string Staff_Phone)
        {
            return await _staffRepository.Get(Staff_ID, IsActive, StaffGroup_ID, Username, Password, Staff_Phone);
        }

        public async Task Create(Staff staff)
        {
            await _staffRepository.Create(staff);
        }



        public async Task Update(Staff staff)
        {
            await _staffRepository.Update(staff);
        }
    }
}
