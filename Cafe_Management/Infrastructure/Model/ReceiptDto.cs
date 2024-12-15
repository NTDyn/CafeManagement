namespace Cafe_Management.Infrastructure.Model
{
    public class ReceiptDto
    {
        public int? Receipt_ID { get; set; }
        public int? Staff_ID { get; set; }
        public int? Customer_ID { get; set; }
        public int? TotalPrice { get; set; }
        public int? Cuppon_ID { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }
        public string customer_Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
       
    }
}
