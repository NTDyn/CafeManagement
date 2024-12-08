using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("Receipt")]
    public class Receipt
    {
        [Key]
        public int Receipt_ID { get; set; }
        public int Staff_ID { get; set; }
        public int TotalPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<ReceiptDetail>? Details { get; set; }
    }
}
