using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class SpoiledIngredientService
    {
        private readonly ISpoiledIngredientRepository _spoiledIngredientRepository;

        public SpoiledIngredientService(ISpoiledIngredientRepository spoiledIngredientRepository)
        {
            _spoiledIngredientRepository = spoiledIngredientRepository;
        }

        public async Task<IEnumerable<SpoiledIngredient>> Get(Nullable<int> Spoiled_ID = null)
        {
            return await _spoiledIngredientRepository.Get(Spoiled_ID);
        }

        public async Task Create(SpoiledIngredient SpoiledIngredient)
        {
            await _spoiledIngredientRepository.Create(SpoiledIngredient);
        }
    }
}
