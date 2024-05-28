using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Inválid format Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "The {0} must be least {2} and at max {1} characters long!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
