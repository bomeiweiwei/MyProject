using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    public partial class Announcement : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("AnnouncementNo")]
        [Display(Name = "Announcement.AnnouncementNo")]
        public override int Id { get; set; }

        [Display(Name = "Announcement.EmpNo")]
        public Nullable<int> EmpNo { get; set; }

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Announcement.AnnouncementType")]
        public string AnnouncementType { get; set; }

        [MaxLength(300, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "Announcement.AnnouncementContent")]
        public string AnnouncementContent { get; set; }

        [Display(Name = "Announcement.CreateDate")]
        public System.DateTime CreateDate { get; set; }

        [Display(Name = "Announcement.UpdateDate")]
        public Nullable<System.DateTime> UpdateDate { get; set; }

        [Display(Name = "Announcement.StartDate")]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "Announcement.EndDate")]
        public System.DateTime EndDate { get; set; }

        [ForeignKey("EmpNo")]
        public virtual EmployeeSetting Employee { get; set; }
    }
}
