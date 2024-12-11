using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IRecipeRawRepository
    {
        Task<IEnumerable<RecipeRaw>> GetRecipeRaw(Nullable<int> ingredientResult);
        Task AddRecipeRaw(RecipeRaw recipeRaw);
        Task UpdateRecipeRaw(RecipeRaw recipeRaw);
    }
}
