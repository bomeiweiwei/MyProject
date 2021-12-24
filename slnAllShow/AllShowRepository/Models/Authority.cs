using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    public partial class Authority
    {
        [ScaffoldColumn(false)]
        [Key, Column(Order = 0)]
        public int EmpNo { get; set; }
        [ScaffoldColumn(false)]
        [Key, Column(Order = 1)]
        public int AuthorityNo { get; set; }

        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        public string Note { get; set; }

        [ForeignKey("EmpNo")]
        public virtual EmployeeSetting Employee { get; set; }
        [ForeignKey("AuthorityNo")]
        public virtual AuthorityFunction AuthorityFunction { get; set; }
    }
}
