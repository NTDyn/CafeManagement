using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("CustomerLevels")]
    public class CustomerLevel
    {
        [Key]
        public int? Level_ID { get; set; }
        public string? Level_Name { get; set; }
        public int? PointApply { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
