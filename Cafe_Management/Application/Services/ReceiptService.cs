using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Model;

namespace Cafe_Management.Application.Services
{
    public class ReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;

        public ReceiptService(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public async Task<IEnumerable<Receipt>> Get(Nullable<int> Receipt_ID = null)
        {
            return await _receiptRepository.Get(Receipt_ID);
        }

        public async Task Create(Receipt Receipt)
        {
            await _receiptRepository.Create(Receipt);
        }

        public async Task CreateReceiptCheckout(Receipt receipt)
        {
            await _receiptRepository.CreateReceiptCheckout(receipt);
        }
        public async Task AddCart(ReceiptDetail receiptDetail,int id)
        {
           await _receiptRepository.CreateCart(receiptDetail,id);
        }
        public async Task<IEnumerable<CartDto>>getCartofCustomer(int id)
        {
            return await _receiptRepository.GetCartByIdCustomer(id);
        }
        public async Task ChangeQuantityDetailReceipt(int id, int quantity)
        {
            await _receiptRepository.ChangeQuantity(id, quantity);
        }
        public async Task DeletaDetailReceipt(int id)
        {
            await _receiptRepository.DeleteDetailReceipt(id);
        }
        public async Task ChangeStatusCart(Receipt receipt, List<ReceiptDetail> receiptDetail)
        {
            await _receiptRepository.CheckoutFromCart(receipt, receiptDetail);
        }
        public async Task<IEnumerable<ReceiptDto>>getListReceiptByStatus(int status)
        {
            return await _receiptRepository.getReceiptByStatus(status);
        }
        public async Task <IEnumerable<CartDto>>getDetailReceipt(int id)
        {
            return await _receiptRepository.GetDetailReceiptById(id);
        }
     
    }
}
