using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class CupponService
    {
        private readonly ICupponRepository _cupponRepository;

        public CupponService(ICupponRepository cupponRepository)
        {
            _cupponRepository = cupponRepository;
        }

        public async Task<IEnumerable<Cuppon>> GetCuppons(Nullable<int> Cuppon_ID, Nullable<bool> IsActive)
        {
            return await _cupponRepository.GetCuppons(Cuppon_ID, IsActive);
        }

        public async Task AddCuppon(Cuppon cuppon)
        {
            await _cupponRepository.AddCuppon(cuppon);
        }



        public async Task UpdateCuppon(Cuppon cuppon)
        {
            await _cupponRepository.UpdateCuppon(cuppon);
        }
    }
}
