using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    public class AuthorityFunction : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("AuthorityNo")]
        [Display(Name = "AuthorityFunction.AuthorityNo")]
        public override int Id { get; set; }

        [MaxLength(40, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "AuthorityFunction.AuthorityName")]
        public string AuthorityName { get; set; }

        public ICollection<Authority> Authority { get; set; }
    }
}
