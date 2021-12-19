using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjAllShow.Backend.Models
{
    public class ProductClass : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("ProClassNo")]
        [Display(Name = "ProductClass.ProClassNo")]
        public override int Id { get; set; }

        [Display(Name = "ProductClass.ShClassNo")]
        public int ShClassNo { get; set; }

        //public int ShNo { get; set; } 

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "ProductClass.ProClassName")]
        public string ProClassName { get; set; }

        [ForeignKey("ShClassNo")]
        public virtual ShClass ShClass { get; set; }
    }
}
