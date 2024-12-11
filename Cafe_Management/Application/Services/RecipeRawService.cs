using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class RecipeRawService
    {
        private readonly IRecipeRawRepository _recipeRawRepository;

        public RecipeRawService(IRecipeRawRepository recipeRawRepository)
        {
            _recipeRawRepository = recipeRawRepository;
        }

        public async Task<IEnumerable<RecipeRaw>> GetRecipeRaw(Nullable<int> ingredientResult)
        {
            return await _recipeRawRepository.GetRecipeRaw(ingredientResult);
        }

        public async Task AddRecipeRaw(RecipeRaw recipeRaw)
        {
            await _recipeRawRepository.AddRecipeRaw(recipeRaw);
        }



        public async Task UpdateRecipeRaw(RecipeRaw recipeRaw)
        {
            await _recipeRawRepository.UpdateRecipeRaw(recipeRaw);
        }
    }
}
