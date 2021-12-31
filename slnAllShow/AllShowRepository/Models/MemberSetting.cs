using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    [Table("Member")]
    public partial class MemberSetting : BaseModel
    {
        [Key, Column("MemNo")]
        public override int Id { get; set; }

        [MaxLength(256)]
        public string MemEmail { get; set; }

        public string MemPwd { get; set; }

        [MaxLength(40)]
        public string MemDiminutive { get; set; }

        [MaxLength(40)]
        public string MemName { get; set; }

        [MaxLength(1)]
        public string MemSex { get; set; }

        [MaxLength(10)]
        public string MemTel { get; set; }

        [MaxLength(80)]
        public string MemAddress { get; set; }

        [MaxLength(1000)]
        public string MemPic { get; set; }

        [MaxLength(1)]
        public string MemAccountState { get; set; }

        [MaxLength(5)]
        public string MemCheckNumber { get; set; }

        public System.DateTime MemCreateDate { get; set; }

        public Nullable<System.DateTime> MemBirth { get; set; }

        public Nullable<System.DateTime> MemUpdateDate { get; set; }

        public virtual ICollection<FavoriteShopList> FavoriteShopList { get; set; }
        public virtual ICollection<MemberList> MemberList { get; set; }
    }
}
