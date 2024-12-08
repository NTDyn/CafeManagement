using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("BatchRecipe")]
    public class BatchRecipe
    {
        [Key]
        public int? BatchRecipe_ID { get; set; }
        public int? IngredientResult_ID { get; set; }
        public double? Quality { get; set; }
        public int? Unit { get; set; } // 1 = min, 2 = transfer, 3 = max
        public int? Staff_ID { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<BatchRecipeDetail>? Details { get; set; }
    }
}
