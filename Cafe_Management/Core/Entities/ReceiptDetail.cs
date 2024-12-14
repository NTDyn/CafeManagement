using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("RecipeDetails")]
    public class ReceiptDetail
    {
        [Key]
        public int Detail_ID { get; set; }
        public int Receipt_ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }
        public int status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
