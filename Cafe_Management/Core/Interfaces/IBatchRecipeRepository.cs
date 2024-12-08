using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IBatchRecipeRepository
    {
        Task<IEnumerable<BatchRecipe>> Get(Nullable<int> BatchRecipe_ID = null);
        Task Create(BatchRecipe BatchRecipe);
    }
}
