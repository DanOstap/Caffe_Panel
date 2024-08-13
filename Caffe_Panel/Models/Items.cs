using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caffe_Panel.Models
{
    [Table("Items")]
    public class Items
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ID { get; set; }
        public string Product_Name { get; set; }
        public double Product_Price { get; set; }
        public int Product_Quantity { get; set; }
        public int Product_Discount { get; set; } = 0;


    }
}
