using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IMenuDetailRepository
    {
        Task<IEnumerable<MenuDetail>> GetMenuDetail(Nullable<int> Menu_ID);
        Task Create(List<MenuDetail> menuDetails);
        Task Update(List<MenuDetail> menuDetails);
    }
}
