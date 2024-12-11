using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int? Customer_Id { get; set; }
        public string? Customer_Name { get; set; }
        public string? Customer_Phone { get; set; }
        public string? Customer_Email { get; set; }
        public string? Customer_Address { get; set; }
        public int? Customer_Point { get; set; }
        public int? Level_ID { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
