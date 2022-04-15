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
    public partial class AuthorityFunction : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("AuthorityNo")]
        [Display(Name = "AuthorityFunction.AuthorityNo")]
        public override int Id { get; set; }

        [MaxLength(40, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "AuthorityFunction.AuthorityName")]
        public string AuthorityName { get; set; }

        [JsonIgnore]
        public ICollection<Authority> Authority { get; set; }

    }
}
