using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IStoreIngredientRepository
    {
        Task<IEnumerable<StoreIngredient>> Get(Nullable<int> Ingredient_ID = null, Nullable<int> Warehouse_ID = null);
        Task Create(StoreIngredient storeIngredient);
        Task Update(StoreIngredient storeIngredient);
    }
}
