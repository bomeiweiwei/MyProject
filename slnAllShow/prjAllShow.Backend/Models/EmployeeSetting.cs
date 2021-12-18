using prjAllShow.Backend.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    public class EmployeeSetting : BaseModel
    {
        public override int Id { get; set; }
        public string EmpName { get; set; }
        public string EmpAccount { get; set; }
        public string EmpPwd { get; set; }
        public string EmpEmail { get; set; }
        public string EmpSex { get; set; }
        public System.DateTime EmpBirth { get; set; }
        public string EmpTel { get; set; }
        public System.DateTime HireDate { get; set; }
        public Nullable<System.DateTime> LeaveDate { get; set; }
        public string EmpAccountState { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }

    }
}
