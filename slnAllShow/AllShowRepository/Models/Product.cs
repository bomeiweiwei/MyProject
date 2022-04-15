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
    public partial class Product : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("ProNo")]
        [Display(Name = "Product.ProNo")]
        public override int Id { get; set; }

        [Display(Name = "Product.ShNo")]
        public Nullable<int> ShNo { get; set; }

        [Display(Name = "Product.ProClassNo")]
        public Nullable<int> ProClassNo { get; set; }

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Product.ProName")]
        public string ProName { get; set; }

        [Display(Name = "Product.ProPrice")]
        public int ProPrice { get; set; }

        [MaxLength(200, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Product.ProStatement")]
        public string ProStatement { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Product.ProState")]
        public string ProState { get; set; }

        [MaxLength(1000, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Product.ProPic1")]
        public string ProPic1 { get; set; }

        [MaxLength(1000, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Product.ProPic2")]
        public string ProPic2 { get; set; }

        [MaxLength(1000, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Product.ProPic3")]
        public string ProPic3 { get; set; }

        [Display(Name = "Product.ProCreateDate")]
        public Nullable<System.DateTime> ProCreateDate { get; set; }

        [Display(Name = "Product.ProUpdateDate")]
        public Nullable<System.DateTime> ProUpdateDate { get; set; }

        [Display(Name = "Product.ProOffshelfDate")]
        public Nullable<System.DateTime> ProOffshelfDate { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Product.ProPop")]
        public string ProPop { get; set; }

        [JsonIgnore]
        [ForeignKey("ProClassNo")]
        public virtual ProductClass ProductClass { get; set; }
        [JsonIgnore]
        [ForeignKey("ShNo")]
        public virtual ShopSetting Shop { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderList> OrderList { get; set; }
    }
}
