using prjAllShow.Backend.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    public class ShopSetting : BaseModel
    {
        public override int Id { get; set; }
        public Nullable<int> EmpNo { get; set; }
        public string ShThePic { get; set; }
        public string ShName { get; set; }
        public string ShAccount { get; set; }
        public string ShPwd { get; set; }
        public string ShBoss { get; set; }
        public string ShContact { get; set; }
        public string ShAddress { get; set; }
        public string ShTel { get; set; }
        public string ShEmail { get; set; }
        public string ShAbout { get; set; }
        public string ShLogoPic { get; set; }
        public string ShUrl { get; set; }
        public string ShAdState { get; set; }
        public string ShAdTitle { get; set; }
        public string ShAdPic { get; set; }
        public string ShPopShop { get; set; }
        public string ShCheckState { get; set; }
        public Nullable<System.DateTime> ShStartDate { get; set; }
        public Nullable<System.DateTime> ShEndDate { get; set; }
        public Nullable<System.DateTime> ShCheckDate { get; set; }
        public string ShPwdState { get; set; }
        public Nullable<System.DateTime> ShStopRightStartDate { get; set; }
        public Nullable<System.DateTime> ShStopRightEnddate { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
    }
}
