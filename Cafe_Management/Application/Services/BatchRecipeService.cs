using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class BatchRecipeService
    {
        private readonly IBatchRecipeRepository _batchRecipeRepository;

        public BatchRecipeService(IBatchRecipeRepository batchRecipeRepository)
        {
            _batchRecipeRepository = batchRecipeRepository;
        }

        public async Task<IEnumerable<BatchRecipe>> Get(Nullable<int> BatchRecipe_ID = null)
        {
            return await _batchRecipeRepository.Get(BatchRecipe_ID);
        }

        public async Task Create(BatchRecipe BatchRecipe)
        {
            await _batchRecipeRepository.Create(BatchRecipe);
        }
    }
}
