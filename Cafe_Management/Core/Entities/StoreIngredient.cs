using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("StoreIngredients")]
    public class StoreIngredient
    {
        [Key]
        public int? Store_ID { get; set; }
        public int? Warehouse_ID { get; set; }
        public int? Ingredient_ID { get; set; }
        public int? Price { get; set; }
        public double? Quality { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
