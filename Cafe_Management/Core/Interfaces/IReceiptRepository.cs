using Cafe_Management.Core.Entities;

namespace Cafe_Management.Core.Interfaces
{
    public interface IReceiptRepository
    {
        Task<IEnumerable<Receipt>> Get(Nullable<int> Receipt_ID = null);
        Task Create(Receipt Receipt );
        //Task Update(Receipt Receipt);
        Task CreateReceiptCheckout(Receipt receipt);
        Task CreateCart(ReceiptDetail receiptDetai, int id);
        Task<IEnumerable<Receipt>> GetCartByIdCustomer(int id);
    }
}
