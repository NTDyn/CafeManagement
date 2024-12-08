using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("Staffs")]
    public class Staff
    {
        [Key]
        public int? Staff_ID { get; set; }
        public int? StaffGroup_ID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Staff_FullName { get; set; }
        public string? Staff_Phone { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
