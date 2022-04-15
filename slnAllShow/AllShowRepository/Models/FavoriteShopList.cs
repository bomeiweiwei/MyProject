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
    public partial class FavoriteShopList
    {
        [ScaffoldColumn(false)]
        [Key, Column(Order = 1)]
        public int MemNo { get; set; }
        [ScaffoldColumn(false)]
        [Key, Column(Order = 0)]
        public int ShNo { get; set; }
        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        public string Note { get; set; }

        [JsonIgnore]
        [ForeignKey("MemNo")]
        public virtual MemberSetting Member { get; set; }
        [JsonIgnore]
        [ForeignKey("ShNo")]
        public virtual ShopSetting Shop { get; set; }
    }
}
