using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowDTO
{
    public class ShopSettingDTO : BaseModel
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
        //[Required(ErrorMessage = "Field_Required")]
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Shop.ShStartDate")]
        public Nullable<System.DateTime> ShStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Shop.ShEndDate")]
        public Nullable<System.DateTime> ShEndDate { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Shop.ShCheckDate")]
        public Nullable<System.DateTime> ShCheckDate { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Shop.ShPwdState")]
        public string ShPwdState { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Shop.ShStopRightStartDate")]
        public Nullable<System.DateTime> ShStopRightStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Shop.ShStopRightEnddate")]
        public Nullable<System.DateTime> ShStopRightEnddate { get; set; }

        public int AuserId { get; set; }

        [Display(Name = "Shop.ShAdState")]
        public string ShAdStateDesc
        {
            get
            {
                if (ShAdState == "1")
                {
                    return "顯示";
                }
                else
                {
                    return "未顯示";
                }
            }
        }

        [Display(Name = "Shop.ShPopShop")]
        public string ShPopShopDesc
        {
            get
            {
                if (ShPopShop == "1")
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
        }

        [Display(Name = "Shop.ShCheckState")]
        public string ShCheckStateDesc
        {
            get
            {
                if (ShCheckState == "1")
                {
                    return "已通過審查";
                }
                else
                {
                    return "未審查";
                }
            }
        }

        [Display(Name = "Shop.ShPwdState")]
        public string ShPwdStateDesc
        {
            get
            {
                if (ShPwdState == "1")
                {
                    return "啟用";
                }
                else
                {
                    return "停用";
                }
            }
        }

        [Display(Name = "Employee.ApproveEmpName")]
        public string EmpName { get; set; }

        public bool? ChangePwd { get; set; }

        [Display(Name = "ShClass.ShClassName")]
        public string ShClassName { get; set; }

        public List<string> ShClassListID { get; set; }
    }
}
