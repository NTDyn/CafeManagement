using Cafe_Management.Core.Entities;

namespace Cafe_Management.Infrastructure.Model
{
    public class CheckoutFromCartDto
    {
        public Receipt receipt { get; set; }
        public List<ReceiptDetail>listReceipt  {get;set;}
    }
}
