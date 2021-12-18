using System.ComponentModel.DataAnnotations;

namespace prjAllShow.Backend.Models
{
    public class EditUserViewModel
    {
        public string Email { get; set;}
        [Required]
        public string UserName { get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}
        public string PhoneNumber { get; set;}
        public bool? ChangePwd { get; set; }
    }
}
