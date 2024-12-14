using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("HistoryDisscount")]
    public class HistoryDisscount
    {
        [Key]
        public int History_ID { get; set; }
        public int Customer_ID { get; set; }
        public int Cuppon_ID { get; set; }
        public int PriceDisscount { get; set; }
        public int Receipt_ID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        
    }
}
