using prjAllShow.Backend.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    public class MemberSetting : BaseModel
    {
        public override int Id { get; set; }
        public string MemEmail { get; set; }
        public string MemPwd { get; set; }
        public string MemDiminutive { get; set; }
        public string MemName { get; set; }
        public string MemSex { get; set; }
        public string MemTel { get; set; }
        public string MemAddress { get; set; }
        public string MemPic { get; set; }
        public string MemAccountState { get; set; }
        public string MemCheckNumber { get; set; }
        public System.DateTime MemCreateDate { get; set; }
        public Nullable<System.DateTime> MemBirth { get; set; }
        public Nullable<System.DateTime> MemUpdateDate { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
    }
}