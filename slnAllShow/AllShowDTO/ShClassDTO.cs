using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowDTO
{
    public class ShClassDTO
    {
        public int Id { get; set; }

        [Display(Name = "ShClass.ShClassName")]
        public string ShClassName { get; set; }
    }
}
