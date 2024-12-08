using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Repositories;

namespace Cafe_Management.Application.Services
{
    public class MenuDetailService
    {
        private readonly IMenuDetailRepository _menuDetailRepository;

        public MenuDetailService(IMenuDetailRepository menuDetailRepository)
        {
            _menuDetailRepository = menuDetailRepository;
        }

        public async Task<IEnumerable<MenuDetail>> Get(Nullable<int> menuId)
        {
            return await _menuDetailRepository.GetMenuDetail(menuId);
        }
        public async Task Create(List<MenuDetail> MenuDetails)
        {
            await _menuDetailRepository.Create(MenuDetails);
        }

        public async Task Update(List<MenuDetail> MenuDetails)
        {
            await _menuDetailRepository.Update(MenuDetails);
        }
    }
}
