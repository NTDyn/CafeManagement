﻿using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;

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
        public async Task<IEnumerable<Receipt>>getCartofCustomer(int id)
        {
            return await _receiptRepository.GetCartByIdCustomer(id);
        }
     
    }
}
