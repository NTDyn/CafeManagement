using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Repositories;

namespace Cafe_Management.Application.Services
{
    public class ProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategories(Nullable<int> cateID)
        {
            return await _productCategoryRepository.GetAllProductCategories(cateID);
        }

        public async Task AddProductCategory(ProductCategory category)
        {
            await _productCategoryRepository.AddProductCategory(category);
        }

 

        public async Task UpdateProductCategory(ProductCategory category)
        {
            await _productCategoryRepository.UpdateProductCategory(category);
        }


    }
}
