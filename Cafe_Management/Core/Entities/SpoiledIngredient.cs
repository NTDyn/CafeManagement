using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("SpoiledIngredients")]
    public class SpoiledIngredient
    {
        [Key]
        public int? Spoiled_ID { get; set; }
        public string? Reason { get; set; }
        public int? Staff_ID { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<SpoiledIngredientDetail>? Details { get; set; }
    }
}
