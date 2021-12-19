using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    public class FavoriteShopList
    {
        [ScaffoldColumn(false)]
        [Key, Column(Order = 1)]
        public int MemNo { get; set; }
        [ScaffoldColumn(false)]
        [Key, Column(Order = 0)]
        public int ShNo { get; set; }
        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        public string Note { get; set; }

        [ForeignKey("MemNo")]
        public virtual MemberSetting Member { get; set; }
        [ForeignKey("ShNo")]
        public virtual ShopSetting Shop { get; set; }
    }
}
