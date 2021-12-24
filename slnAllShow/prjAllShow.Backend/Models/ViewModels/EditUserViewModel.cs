using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool? ChangePwd { get; set; }
    }
}
