using System.ComponentModel.DataAnnotations;

namespace EDWeb.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string SecondLastName { get; set; }

        public string JobTitle { get; set; }

        public string Location { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}