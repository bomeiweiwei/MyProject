using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    public partial class OrderList
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

        [JsonIgnore]
        [ForeignKey("ProNo")]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        [ForeignKey("ShoporderNo")]
        public virtual ShopOrder ShopOrder { get; set; }
    }
}
