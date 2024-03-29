﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    public partial class ShopOrder : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("ShoporderNo")]
        [Display(Name = "ShopOrder.ShoporderNo")]
        public override int Id { get; set; }

        [Display(Name = "ShopOrder.OrderNo")]
        public Nullable<int> OrderNo { get; set; }

        [Display(Name = "ShopOrder.ShNo")]
        public Nullable<int> ShNo { get; set; }

        [Display(Name = "ShopOrder.OrderPrice")]
        public int OrderPrice { get; set; }

        [Display(Name = "ShopOrder.ReferredToDate")]
        public Nullable<System.DateTime> ReferredToDate { get; set; }

        [Display(Name = "ShopOrder.TransactionDate")]
        public Nullable<System.DateTime> TransactionDate { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "ShopOrder.OrderState")]
        public string OrderState { get; set; }

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "ShopOrder.RecipientName")]
        public string RecipientName { get; set; }

        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "ShopOrder.RecipientTel")]
        public string RecipientTel { get; set; }

        [MaxLength(50, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "ShopOrder.RecipientAddress")]
        public string RecipientAddress { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "ShopOrder.PayType")]
        public string PayType { get; set; }

        [JsonIgnore]
        [ForeignKey("ShNo")]
        public virtual ShopSetting Shop { get; set; }
        [JsonIgnore]
        [ForeignKey("OrderNo")]
        public virtual MemberList MemberList { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderList> OrderList { get; set; }
    }
}
