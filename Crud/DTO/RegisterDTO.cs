using System.ComponentModel.DataAnnotations;

namespace Crud.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name is requiered.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is requiered.")]
        [EmailAddress(ErrorMessage = "The email is not valid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is requiered.")]
        [MinLength(8, ErrorMessage = "The password must be a minimum of 8 characters.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public int Money { get; set; }
    }
}
