using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace prjAllShow.Backend.Models
{
    [Table("Shop")]
    public class ShopSetting : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("ShNo")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShNo")]
        public override int Id { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.EmpNo")]
        public Nullable<int> EmpNo { get; set; }

        [MaxLength(1000, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShThePic")]
        public string ShThePic { get; set; }

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShName")]
        public string ShName { get; set; }

        //[Required(ErrorMessage = "Field_Required")]
        //[Display(Name = "Shop.ShClassNo")]
        //public int ShClassNo { get; set; } 

        [MaxLength(256, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShAccount")]
        public string ShAccount { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Shop.ShPwd")]
        public string ShPwd { get; set; }

        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShBoss")]
        public string ShBoss { get; set; }

        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShContact")]
        public string ShContact { get; set; }

        [MaxLength(30, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShAddress")]
        public string ShAddress { get; set; }

        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShTel")]
        public string ShTel { get; set; }

        [MaxLength(256, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShEmail")]
        public string ShEmail { get; set; }

        [MaxLength(300, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Shop.ShAbout")]
        public string ShAbout { get; set; }

        [MaxLength(1000, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Shop.ShLogoPic")]
        public string ShLogoPic { get; set; }

        [MaxLength(50, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShUrl")]
        public string ShUrl { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShAdState")]
        public string ShAdState { get; set; }

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Shop.ShAdTitle")]
        public string ShAdTitle { get; set; }

        [MaxLength(1000, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Shop.ShAdPic")]
        public string ShAdPic { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShPopShop")]
        public string ShPopShop { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShCheckState")]
        public string ShCheckState { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShStartDate")]
        public Nullable<System.DateTime> ShStartDate { get; set; }

        [Display(Name = "Shop.ShEndDate")]
        public Nullable<System.DateTime> ShEndDate { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShCheckDate")]
        public Nullable<System.DateTime> ShCheckDate { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Shop.ShPwdState")]
        public string ShPwdState { get; set; }

        [Display(Name = "Shop.ShStopRightStartDate")]
        public Nullable<System.DateTime> ShStopRightStartDate { get; set; }

        [Display(Name = "Shop.ShStopRightEnddate")]
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
