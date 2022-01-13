using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models.Identity
{
    public partial class RefreshToken : BaseModel
    {
        public int UserId { get; set; } // Linked to the AspNet Identity User Id
        [Required]
        [StringLength(256)]
        public string Token { get; set; }
        [Required]
        [StringLength(128)]
        public string JwtId { get; set; } // Map the token with jwtId
        [Required]
        public bool IsUsed { get; set; } // if its used we dont want generate a new Jwt token with the same refresh token
        public bool IsRevoked { get; set; } // if it has been revoke for security reasons
        public bool Invalidated { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; } // Refresh token is long lived it could last for months.

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
