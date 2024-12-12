using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("Cuppons")]
    public class Cuppon
    {
        [Key]
        public int? Cuppon_ID { get; set; }
        public string? Cuppon_Name { get; set; }
        public double? Disscount { get; set; }
        public int? Cuppon_Type { get; set; } // 0 = %, 1 = money
        public int? ApplyLevel_ID { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
