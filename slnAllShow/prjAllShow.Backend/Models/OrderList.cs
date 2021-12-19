using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    public class OrderList
    {
        [ScaffoldColumn(false)]
        [Key, Column(Order = 1)]
        [Display(Name = "OrderList.ShoporderNo")]
        public int ShoporderNo { get; set; }

        [ScaffoldColumn(false)]
        [Key, Column(Order = 0)]
        [Display(Name = "OrderList.ProNo")]
        public int ProNo { get; set; }

        [Display(Name = "OrderList.Quantity")]
        public int Quantity { get; set; }

        [ForeignKey("ProNo")]
        public virtual Product Product { get; set; }
        [ForeignKey("ShoporderNo")]
        public virtual ShopOrder ShopOrder { get; set; }
    }
}
