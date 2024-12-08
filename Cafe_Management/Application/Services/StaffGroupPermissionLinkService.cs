using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class StaffGroupPermissionLinkService
    {
        private readonly IStaffGroupPermissionResponsitory _staffGroupPermissionResponsitory;

        public StaffGroupPermissionLinkService(IStaffGroupPermissionResponsitory staffGroupPermissionResponsitory)
        {
            _staffGroupPermissionResponsitory = staffGroupPermissionResponsitory;
        }

        public async Task<IEnumerable<StaffGroupLinkPermission>> Get(Nullable<int> Permission_ID,
                                                Nullable<bool> IsActive,
                                                Nullable<int> StaffGroup)
        {
            return await _staffGroupPermissionResponsitory.Get(Permission_ID, IsActive, StaffGroup);
        }

        public async Task Create(StaffGroupLinkPermission staff)
        {
            await _staffGroupPermissionResponsitory.Create(staff);
        }



        public async Task Update(StaffGroupLinkPermission staff)
        {
            await _staffGroupPermissionResponsitory.Update(staff);
        }
    }
}
