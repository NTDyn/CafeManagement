using Cafe_Management.Code;
using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IIngredientRepository
    {
        Task<Ingredient?> GetIngredientById(int id);
        Task<IEnumerable<Ingredient>> GetAllIngredients(Nullable<int> Type = null);
        Task AddIngredient(Ingredient ingredient);
        Task UpdateIngredient(Ingredient ingredient);
    }
}
