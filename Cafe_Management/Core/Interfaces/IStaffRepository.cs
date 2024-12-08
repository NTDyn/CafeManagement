using Cafe_Management.Core.Entities;
using Cafe_Management.Infrastructure.Repositories;

namespace Cafe_Management.Core.Interfaces
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> Get(Nullable<int> Staff_ID = null,
                                                Nullable<bool> IsActive = null,
                                                Nullable<int> StaffGroup_ID = null,
                                                string Username = null,
                                                string Password = null,
                                                string Staff_Phone = null);
        Task Create(Staff staff);
        Task Update(Staff staff);
    }
}
