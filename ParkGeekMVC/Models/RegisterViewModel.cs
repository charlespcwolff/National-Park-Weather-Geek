using System.ComponentModel.DataAnnotations;

namespace ParkGeekMVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is a required field")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is a required field")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is a required field")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email address is a required field")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password length must be between {2} and {1} characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }
    }
}