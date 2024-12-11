namespace Cafe_Management.Infrastructure.Model
{
    public class SupplierLinkUpdateDto
    {
        public int link_ID { get; set; }
        public int SupplierID { get; set; }
        public int StaffRequestID { get; set; }
        public int StaffApprovedID { get; set; }
        public int StaffReceivedID { get; set; }
        public int TotalPrice { get; set; }
        public int WarehouseID { get; set; }
        public bool IsActive { get; set; }
    }
}
