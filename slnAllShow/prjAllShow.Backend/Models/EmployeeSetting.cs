using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    [Table("Employee")]
    public class EmployeeSetting : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("EmpNo")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Employee.EmpNo")]
        public override int Id { get; set; }

        [MaxLength(256, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Employee.EmpName")]
        public string EmpName { get; set; }

        [MaxLength(256, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Employee.EmpAccount")]
        public string EmpAccount { get; set; }

        [Required(ErrorMessage = "Field_Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Employee.EmpPwd")]
        public string EmpPwd { get; set; }

        [MaxLength(256, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Employee.EmpEmail")]
        public string EmpEmail { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Employee.EmpSex")]
        public string EmpSex { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Employee.EmpBirth")]
        public System.DateTime EmpBirth { get; set; }

        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Employee.EmpTel")]
        public string EmpTel { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Employee.HireDate")]
        public System.DateTime HireDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Employee.LeaveDate")]
        public Nullable<System.DateTime> LeaveDate { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "Employee.EmpAccountState")]
        public string EmpAccountState { get; set; }

        public ICollection<Authority> Authorities { get; set; }
        public virtual ICollection<Advertisement> Advertisement { get; set; }
        public virtual ICollection<Announcement> Announcement { get; set; }
        public virtual ICollection<ShopSetting> Shop { get; set; }

    }
}
