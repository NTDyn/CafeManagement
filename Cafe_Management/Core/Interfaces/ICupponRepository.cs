using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface ICupponRepository
    {
        Task<IEnumerable<Cuppon>> GetCuppons(Nullable<int> Cuppon_ID, Nullable<bool> IsActive);
        Task AddCuppon(Cuppon cuppon);
        Task UpdateCuppon(Cuppon cuppon);
    }
}
