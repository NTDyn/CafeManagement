namespace Cafe_Management.Infrastructure.Model
{
    public class SupplierDetailUpdateDto
    {
        public int link_ID { get; set; }
       public int detail_Id { get; set; }
        public int IngredientID { get; set; }
        public int Price { get; set; }
        public double Quality { get; set; }
        public int Unit { get; set; }
        public bool IsActive { get; set; }

    }
}
