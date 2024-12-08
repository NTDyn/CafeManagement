namespace Cafe_Management.Infrastructure.Model
{
    public class IngredientSupplierRequest
    {
        public int Link_ID { get; set; }
        public int Supplier_ID { get; set; }
        public int StaffRequest_ID { get; set; }
        public int StaffApproved_ID { get; set; }
        public int StaffReceived_ID { get; set; }
        public int TotalPrice { get; set; }
        public int Status { get; set; }
        public int Warehouse_ID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        /*link*/


        public int Detail_ID { get; set; }
        public int Header_ID { get; set; }
        public int Ingredient_ID { get; set; }
        public int Price { get; set; }
        public double Quality { get; set; }
        public int Unit { get; set; }
        public bool IsActive_Detail  { get; set; }
        public DateTime CreatedDate_Detail  { get; set; }
        public DateTime ModifiedDate_Detail  { get; set; }


    }
}
