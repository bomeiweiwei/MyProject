using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    [Table("Employee")]
    public partial class EmployeeSetting : BaseModel
    {
        [Key, Column("EmpNo")]
        public override int Id { get; set; }

        [MaxLength(256)]
        public string EmpName { get; set; }

        [MaxLength(256)]
        public string EmpAccount { get; set; }

        public string EmpPwd { get; set; }

        [MaxLength(256)]
        public string EmpEmail { get; set; }

        [MaxLength(1)]
        public string EmpSex { get; set; }

        public System.DateTime EmpBirth { get; set; }

        [MaxLength(10)]
        public string EmpTel { get; set; }

        public System.DateTime HireDate { get; set; }

        public Nullable<System.DateTime> LeaveDate { get; set; }

        [MaxLength(1)]
        public string EmpAccountState { get; set; }

        [JsonIgnore]
        public ICollection<Authority> Authorities { get; set; }
        [JsonIgnore]
        public virtual ICollection<Advertisement> Advertisement { get; set; }
        [JsonIgnore]
        public virtual ICollection<Announcement> Announcement { get; set; }
        [JsonIgnore]
        public virtual ICollection<ShopSetting> Shop { get; set; }
    }
}
