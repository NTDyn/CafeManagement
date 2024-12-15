using Cafe_Management.Core.Entities;

namespace Cafe_Management.Infrastructure.Model
{
    public class CartDto
    {
        public int? Receipt_ID { get; set; }
        public int? Customer_ID { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ProductName { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Detail_ID { get; set; }
        public string Product_Image { get; set; }



    }
}
