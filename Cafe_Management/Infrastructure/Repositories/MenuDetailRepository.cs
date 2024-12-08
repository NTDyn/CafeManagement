using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class MenuDetailRepository : IMenuDetailRepository
    {
        private readonly AppDbContext _context;

        public MenuDetailRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MenuDetail>> GetMenuDetail(Nullable<int> menuId)
        {
            var JoinData = await (from sg in _context.MenuDetail.Where(pi => pi.Menu_ID == menuId && pi.IsActive == true)
            join sgp in _context.Products.Where(x => x.IsActive == true)
            on sg.Product_ID equals sgp.Product_ID
            select new MenuDetail
            {
                Menu_ID = sg.Menu_ID,
                Product_ID = sg.Product_ID,
                IsActive = sg.IsActive,
                CreatedDate = sg.CreatedDate,
                ModifiedDate = sg.ModifiedDate,
                Product = sgp
            }).ToListAsync();

            return JoinData;
        }
        public async Task Create(List<MenuDetail> menuDetails)
        {
            foreach(var menu in menuDetails)
            {
                menu.CreatedDate = DateTime.Now;
                menu.ModifiedDate = DateTime.Now;

                await _context.MenuDetail.AddAsync(menu);
            }
           
            await _context.SaveChangesAsync();
        }

        public async Task Update(List<MenuDetail> menuDetails)
        {
            var currentProduct = await _context.MenuDetail
                .Where(pi => pi.Menu_ID == menuDetails[0].Menu_ID && pi.IsActive == true).ToListAsync();

            foreach (var product in menuDetails)
            {

                var existing = currentProduct.FirstOrDefault(r => r.Product_ID == product.Product_ID);

                if (existing != null)
                {
                    existing.ModifiedDate = DateTime.Now;
                }
                else
                {
                    _context.MenuDetail.Add(new MenuDetail
                    {
                        Product_ID = product.Product_ID,
                        Menu_ID = product.Menu_ID,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                }

            }
            //DELETE
            var deleteProduct = menuDetails.Where(itemA => !currentProduct.Any(itemB => itemB.Product_ID == itemA.Product_ID)).ToList();
            foreach (var product in deleteProduct)
            {
                product.IsActive = false;
                product.ModifiedDate = DateTime.Now;
            }
          

            await _context.SaveChangesAsync();
        }
    }
}
