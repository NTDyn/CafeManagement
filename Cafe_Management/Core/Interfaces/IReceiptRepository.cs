using Cafe_Management.Core.Entities;
using Cafe_Management.Infrastructure.Model;

namespace Cafe_Management.Core.Interfaces
{
    public interface IReceiptRepository
    {
        Task<IEnumerable<Receipt>> Get(Nullable<int> Receipt_ID = null);
        Task Create(Receipt Receipt);
        //Task Update(Receipt Receipt);
        Task CreateReceiptCheckout(Receipt receipt);
        Task CreateCart(ReceiptDetail receiptDetai, int id);
        Task<IEnumerable<CartDto>> GetCartByIdCustomer(int id);
        Task ChangeQuantity(int id, int quantity);
        Task DeleteDetailReceipt(int id);
        Task CheckoutFromCart(Receipt receipt, List<ReceiptDetail> receiptDetail);
        Task<IEnumerable<ReceiptDto>> getReceiptByStatus(int status);
        Task<IEnumerable<CartDto>> GetDetailReceiptById(int id);
        Task handleDeny(Receipt receipt, List<ReceiptDetail> receiptDetail);
        Task<IEnumerable<ReceiptDto>> showReceipteInAdmin();
    }
     
}
