using System.ComponentModel.DataAnnotations;

namespace prjAllShow.Backend.Models.ViewModels
{
    public class EmployeeViewModel : EmployeeSetting
    {
        public int AuserId { get; set; }

        [Display(Name = "Employee.EmpSex")]
        public string EmpSexDesc {
            get
            {
                if (EmpSex == "1")
                {
                    return "男";
                }
                else
                {
                    return "女";
                }
            }
        }
        [Display(Name = "Employee.EmpAccountState")]
        public string EmpAccountStateDesc {
            get
            {
                if (EmpAccountState == "1")
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
