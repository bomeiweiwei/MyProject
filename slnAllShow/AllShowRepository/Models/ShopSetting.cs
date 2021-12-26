using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    [Table("Shop")]
    public partial class ShopSetting : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("ShNo")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShNo")]
        public override int Id { get; set; }

        public Nullable<int> EmpNo { get; set; }

        [MaxLength(1000)]
        public string ShThePic { get; set; }

        [MaxLength(20)]
        public string ShName { get; set; }

        //[Required(ErrorMessage = "Field_Required")]
        //[Display(Name = "Shop.ShClassNo")]
        //public int ShClassNo { get; set; } 

        [MaxLength(256)]
        public string ShAccount { get; set; }

        public string ShPwd { get; set; }

        [MaxLength(10)]
        public string ShBoss { get; set; }

        [MaxLength(10)]
        public string ShContact { get; set; }

        [MaxLength(30)]
        public string ShAddress { get; set; }

        [MaxLength(10)]
        public string ShTel { get; set; }

        [MaxLength(256)]
        public string ShEmail { get; set; }

        [MaxLength(300)]
        public string ShAbout { get; set; }

        [MaxLength(1000)]
        public string ShLogoPic { get; set; }

        [MaxLength(50)]
        public string ShUrl { get; set; }

        [MaxLength(1)]
        public string ShAdState { get; set; }

        [MaxLength(20)]
        public string ShAdTitle { get; set; }

        [MaxLength(1000)]
        public string ShAdPic { get; set; }

        [MaxLength(1)]
        public string ShPopShop { get; set; }

        [MaxLength(1)]
        public string ShCheckState { get; set; }

        public Nullable<System.DateTime> ShStartDate { get; set; }

        public Nullable<System.DateTime> ShEndDate { get; set; }

        public Nullable<System.DateTime> ShCheckDate { get; set; }

        [MaxLength(1)]
        public string ShPwdState { get; set; }

        public Nullable<System.DateTime> ShStopRightStartDate { get; set; }

        public Nullable<System.DateTime> ShStopRightEnddate { get; set; }

        [ForeignKey("EmpNo")]
        public virtual EmployeeSetting Employee { get; set; }
        public virtual ICollection<Advertisement> Advertisement { get; set; }
        public virtual ICollection<FavoriteShopList> FavoriteShopList { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        //public virtual ICollection<ProductClass> ProductClass { get; set; }
        public virtual ICollection<ShopOrder> ShopOrder { get; set; }
        public virtual ICollection<ShClassList> ShClassList { get; set; }
    }
}
