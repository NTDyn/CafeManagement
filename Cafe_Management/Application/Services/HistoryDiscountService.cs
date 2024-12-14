using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

namespace Cafe_Management.Application.Services
{
    public class HistoryDiscountService
    {
        private readonly IHistoryDisccountRepository _historyDiscountRepository;

        public HistoryDiscountService(IHistoryDisccountRepository historyDiscountRepository)
        {
            _historyDiscountRepository = historyDiscountRepository;
        }

        public async Task<IEnumerable<HistoryDisscount>> GetHistoryDiscounts(Nullable<int> History_ID, Nullable<int> Customer_ID)
        {
            return await _historyDiscountRepository.GetHistoryDiscounts(History_ID, Customer_ID);
        }

        public async Task AddHistoryDiscount(HistoryDisscount historyDisscount)
        {
            await _historyDiscountRepository.AddHistoryDiscount(historyDisscount);
        }



        public async Task UpdateHistoryDiscount(HistoryDisscount historyDisscount)
        {
            await _historyDiscountRepository.UpdateHistoryDiscount(historyDisscount);
        }
    }
}
