using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("StaffGroupLinkPermissions")]
    public class StaffGroupLinkPermission
    {
        [Key]
        public int Link_ID { get; set; }
        public int Permission_ID { get; set; }
        public int StaffGroup { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
