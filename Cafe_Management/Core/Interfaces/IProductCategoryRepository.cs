using Cafe_Management.Code;
using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetAllProductCategories(Nullable<int> CategoryID);


        Task AddProductCategory(ProductCategory  category);

        Task UpdateProductCategory(ProductCategory category);


    }
}
