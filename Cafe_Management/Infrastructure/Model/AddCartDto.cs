using Cafe_Management.Core.Entities;

namespace Cafe_Management.Infrastructure.Model
{
    public class AddCartDto
    {
        public ReceiptDetail receiptDetail { get; set; }
        public int id_customer { get; set; }
    }
}
