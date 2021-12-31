using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowDTO
{
    public class MemberSettingDTO : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("MemNo")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Member.MemNo")]
        public override int Id { get; set; }

        [MaxLength(256)]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Member.MemEmail")]
        public string MemEmail { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Member.MemPwd")]
        public string MemPwd { get; set; }

        [MaxLength(40, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Member.MemDiminutive")]
        public string MemDiminutive { get; set; }

        [MaxLength(40, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Member.MemName")]
        public string MemName { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Member.MemSex")]
        public string MemSex { get; set; }

        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Member.MemTel")]
        public string MemTel { get; set; }

        [MaxLength(80, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Member.MemAddress")]
        public string MemAddress { get; set; }

        [MaxLength(1000, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Member.MemPic")]
        public string MemPic { get; set; }

        [MaxLength(1, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Member.MemAccountState")]
        public string MemAccountState { get; set; }

        [MaxLength(5, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Member.MemCheckNumber")]
        public string MemCheckNumber { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Member.MemCreateDate")]
        public System.DateTime MemCreateDate { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Member.MemBirth")]
        public Nullable<System.DateTime> MemBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Member.MemUpdateDate")]
        public Nullable<System.DateTime> MemUpdateDate { get; set; }

        public int AuserId { get; set; }

        public string MemSexDesc
        {
            get 
            { 
                if (MemSex == "1")
                {
                    return "男";
                }
                else
                {
                    return "女";
                } 
            }
        }

        public string MemAccountStateDesc
        {
            get
            {
                if (MemAccountState == "1")
                {
                    return "啟用";
                }
                else
                {
                    return "停用";
                }
            }
        }

        public bool? ChangePwd { get; set; }
    }
}
