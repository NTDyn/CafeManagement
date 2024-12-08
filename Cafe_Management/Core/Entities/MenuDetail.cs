using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Management.Core.Entities
{
    [Table("MenuDetail")]
    public class MenuDetail
    {
        [Key]
        public int? Setup_ID { get; set; }
        public int? Menu_ID { get; set; }
        public int? Product_ID { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Product? Product { get; set; }


    }
}
