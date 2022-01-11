using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models.Identity
{
    public class AuthResult
    {
        public string JwtId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpireTime { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
