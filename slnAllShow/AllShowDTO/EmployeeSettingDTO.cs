﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowDTO
{
    public class EmployeeSettingDTO : BaseModel
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
