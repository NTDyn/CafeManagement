namespace Cafe_Management.Infrastructure.Model
{
    public class DetaiImportWithIngredientDto
    {
        public int Detail_ID { get; set; }
        public int Header_ID { get; set; }
        public int Ingredient_ID { get; set; }
        public int Price { get; set; }
        public double Quality { get; set; }
        public string? Ingredient_Name { get; set; }
        public int Unit { get; set; }
        public string? Unit_Transfer { get; set; }
        public string? Unit_Min { get; set; }
        public string? Unit_Max { get; set; }
    }
}
