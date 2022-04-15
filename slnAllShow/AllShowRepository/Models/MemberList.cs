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
    public partial class MemberList : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("OrderNo")]
        [Display(Name = "MemberList.OrderNo")]
        public override int Id { get; set; }

        [Display(Name = "MemberList.MemNo")]
        public Nullable<int> MemNo { get; set; }

        [Display(Name = "MemberList.OrderDate")]
        public Nullable<System.DateTime> OrderDate { get; set; }

        [JsonIgnore]
        [ForeignKey("MemNo")]
        public virtual MemberSetting Member { get; set; }
        [JsonIgnore]
        public virtual ICollection<ShopOrder> ShopOrder { get; set; }
    }
}
