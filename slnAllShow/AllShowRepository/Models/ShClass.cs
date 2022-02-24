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
    public partial class ShClass : BaseModel
    {
        [ScaffoldColumn(false)]
        [Key, Column("ShClassNo")]
        [Display(Name = "ShClass.ShClassNo")]
        public override int Id { get; set; }

        [MaxLength(20, ErrorMessage = "Field_MaxLength")]
        [Display(Name = "ShClass.ShClassName")]
        public string ShClassName { get; set; }

        [JsonIgnore]
        public virtual ICollection<ShClassList> ShClassList { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductClass> ProductClass { get; set; }
    }
}
