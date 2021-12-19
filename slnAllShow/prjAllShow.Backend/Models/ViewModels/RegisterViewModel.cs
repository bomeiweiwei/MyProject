using System.ComponentModel.DataAnnotations;

namespace prjAllShow.Backend.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Field_Required")]
        [EmailAddress]
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
