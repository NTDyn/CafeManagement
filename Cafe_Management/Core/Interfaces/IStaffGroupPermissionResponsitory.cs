using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IStaffGroupPermissionResponsitory
    {
        Task<IEnumerable<StaffGroupLinkPermission>> Get(Nullable<int> Permission_ID,
                                                Nullable<bool> IsActive,
                                                Nullable<int> StaffGroup);
        Task Create(StaffGroupLinkPermission staffGroupLinkPermission);
        Task Update(StaffGroupLinkPermission staffGroupLinkPermission);
    }
}
