using System.ComponentModel.DataAnnotations;
using BidingManagementSystem.Domain.Enums;
namespace BidingManagementSystem.Application.DTOs.Users
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Full name is required")]
        [MinLength(3, ErrorMessage = "Full name must be at least 3 characters")]
        public string FullName { get; set; } = default!;
       [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = default!;
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
         public string ConfirmPassword { get; set; } = default!;
         public UserRole Role { get; set; }

    }
}
