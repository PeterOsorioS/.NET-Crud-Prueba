using System.ComponentModel.DataAnnotations;

namespace Crud.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is requiered.")]
        [EmailAddress(ErrorMessage ="The email is not valid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is requiered.")]
        public string Password { get; set; }
    }
}
