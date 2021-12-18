using Microsoft.AspNetCore.Identity;

namespace prjAllShow.Backend.Models.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public bool IsAdmin { get; set; }

        public virtual ICollection<EmployeeSetting> EmployeeSettings { get; set; }
        public virtual ICollection<ShopSetting> ShopSettings { get; set; }
        public virtual ICollection<MemberSetting> MemberSettings { get; set; }
    }
}
