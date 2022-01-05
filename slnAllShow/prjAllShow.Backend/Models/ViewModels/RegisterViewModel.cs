using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Field_Required")]
        [EmailAddress(ErrorMessage = "Plz input Email")]
        [Display(Name = "RegisterViewModel.Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "Field_MaxLength")]
        [StringLength(100, ErrorMessage = "Field_StringLength", MinimumLength = 6)]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "RegisterViewModel.Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Compare_PWD")]
        [Required(ErrorMessage = "Field_Required")]
        [Display(Name = "RegisterViewModel.ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
