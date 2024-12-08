using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class StoreIngredientService
    {
        private readonly IStoreIngredientRepository _storeIngredientRepository;

        public StoreIngredientService(IStoreIngredientRepository storeIngredientRepository)
        {
            _storeIngredientRepository = storeIngredientRepository;
        }

        public async Task<IEnumerable<StoreIngredient>> Get(Nullable<int> Ingredient_ID = null, Nullable<int> Warehouse_ID = null)
        {
            return await _storeIngredientRepository.Get(Ingredient_ID, Warehouse_ID);
        }

        public async Task Create(StoreIngredient StoreIngredient)
        {
            await _storeIngredientRepository.Create(StoreIngredient);
        }

        public async Task Update(StoreIngredient StoreIngredient)
        {
            await _storeIngredientRepository.Update(StoreIngredient);
        }
    }
}
