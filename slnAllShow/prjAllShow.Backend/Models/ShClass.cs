using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    public class ShClass : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("ShClassNo")]
        [Display(Name = "ShClass.ShClassNo")]
        public override int Id { get; set; }

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "ShClass.ShClassName")]
        public string ShClassName { get; set; }

        public virtual ICollection<ShClassList> ShClassList { get; set; }
        public virtual ICollection<ProductClass> ProductClass { get; set; }
    }
}
