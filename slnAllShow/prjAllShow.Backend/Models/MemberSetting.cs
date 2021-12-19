using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    [Table("Member")]
    public class MemberSetting : BaseModel
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
        [Display(Name = "Member.MemCreateDate")]
        public System.DateTime MemCreateDate { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Member.MemBirth")]
        public Nullable<System.DateTime> MemBirth { get; set; }

        [Display(Name = "Member.MemUpdateDate")]
        public Nullable<System.DateTime> MemUpdateDate { get; set; }

        public virtual ICollection<FavoriteShopList> FavoriteShopList { get; set; }
        public virtual ICollection<MemberList> MemberList { get; set; }
    }
}