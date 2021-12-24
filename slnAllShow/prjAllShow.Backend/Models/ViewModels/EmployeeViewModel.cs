using AllShowDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models.ViewModels
{
    public class EmployeeViewModel : EmployeeSettingDTO
    {
        public int AuserId { get; set; }

        [Display(Name = "Employee.EmpSex")]
        public string EmpSexDesc
        {
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
        public string EmpAccountStateDesc
        {
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
