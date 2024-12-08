using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class StaffGroupService
    {
        private readonly IStaffGroupRepository _staffGroupRepository;

        public StaffGroupService(IStaffGroupRepository staffGroupRepository)
        {
            _staffGroupRepository = staffGroupRepository;
        }

        public async Task<IEnumerable<StaffGroup>> Get(Nullable<int> StaffGroup_ID,
                                                Nullable<bool> IsActive)
        {
            return await _staffGroupRepository.Get(StaffGroup_ID, IsActive);
        }

        public async Task Create(StaffGroup staffGroup)
        {
            await _staffGroupRepository.Create(staffGroup);
        }



        public async Task Update(StaffGroup staffGroup)
        {
            await _staffGroupRepository.Update(staffGroup);
        }
    }
}
