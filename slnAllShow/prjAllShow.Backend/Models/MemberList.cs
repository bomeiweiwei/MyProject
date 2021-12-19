﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    public class MemberList : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("OrderNo")]
        [Display(Name = "MemberList.OrderNo")]
        public override int Id { get; set; }

        [Display(Name = "MemberList.MemNo")]
        public Nullable<int> MemNo { get; set; }

        [Display(Name = "MemberList.OrderDate")]
        public Nullable<System.DateTime> OrderDate { get; set; }

        [ForeignKey("MemNo")]
        public virtual MemberSetting Member { get; set; }
        public virtual ICollection<ShopOrder> ShopOrder { get; set; }
    }
}
