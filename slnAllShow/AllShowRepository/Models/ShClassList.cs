using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    public partial class ShClassList
    {
        [ScaffoldColumn(false)]
        [Key, Column(Order = 0)]
        public int ShClassNo { get; set; }

        [ScaffoldColumn(false)]
        [Key, Column(Order = 1)]
        public int ShNo { get; set; }

        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        public string Note { get; set; }

        [ForeignKey("ShClassNo")]
        public virtual ShClass ShClass { get; set; }
        [ForeignKey("ShNo")]
        public virtual ShopSetting Shop { get; set; }
    }
}
