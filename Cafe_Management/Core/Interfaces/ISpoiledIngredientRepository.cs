using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface ISpoiledIngredientRepository
    {
        Task<IEnumerable<SpoiledIngredient>> Get(Nullable<int> Spoiled_ID = null);
        Task Create(SpoiledIngredient SpoiledIngredient);
    }
}
