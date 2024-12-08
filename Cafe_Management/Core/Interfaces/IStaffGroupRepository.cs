using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IStaffGroupRepository
    {
        Task<IEnumerable<StaffGroup>> Get(Nullable<int> StaffGroup_ID,
                                          Nullable<bool> IsActive);
        Task Create(StaffGroup staffGroup);
        Task Update(StaffGroup staffGroup);
    }
}
