using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("StaffGroups")]
    public class StaffGroup
    {
        [Key]
        public int? StaffGroup_ID { get; set; }
        public string? StaffGroup_Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<StaffGroupLinkPermission>? Permissions { get; set; }
    }
}
