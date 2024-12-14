using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IHistoryDisccountRepository
    {
        Task<IEnumerable<HistoryDisscount>> GetHistoryDiscounts(Nullable<int>History_ID,Nullable<int> Customer_ID);
        Task AddHistoryDiscount(HistoryDisscount historyDiscount);
        Task UpdateHistoryDiscount(HistoryDisscount historyDisscount);
    }
}
