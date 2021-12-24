using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    public partial class Advertisement : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("AdNo")]
        [Display(Name = "Advertisement.AdNo")]
        public override int Id { get; set; }

        [Display(Name = "Advertisement.ShNo")]
        public Nullable<int> ShNo { get; set; }

        [Display(Name = "Advertisement.EmpNo")]
        public Nullable<int> EmpNo { get; set; }

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Advertisement.AdTitle")]
        public string AdTitle { get; set; }

        [Display(Name = "Advertisement.AdApplyDate")]
        public System.DateTime AdApplyDate { get; set; }

        [Display(Name = "Advertisement.AdStartDate")]
        public System.DateTime AdStartDate { get; set; }

        [Display(Name = "Advertisement.AdTime")]
        public System.DateTime AdTime { get; set; }

        [Display(Name = "Advertisement.AdPrice")]
        public int AdPrice { get; set; }

        [MaxLength(1000, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Advertisement.AdPic")]
        public string AdPic { get; set; }

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Advertisement.AdURL")]
        public string AdURL { get; set; }

        [MaxLength(10, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Advertisement.AdCheckState")]
        public string AdCheckState { get; set; }

        [ForeignKey("EmpNo")]
        public virtual EmployeeSetting Employee { get; set; }
        [ForeignKey("ShNo")]
        public virtual ShopSetting Shop { get; set; }
    }
}
