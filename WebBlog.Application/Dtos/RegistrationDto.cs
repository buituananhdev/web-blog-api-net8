using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebBog.Application.Dtos
{
    public class RegistrationDto
    {
        [JsonPropertyName("full_name")]
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name can't be longer than 100 characters")]
        public string? FullName { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string? Password { get; set; }

        [JsonPropertyName("confirm_password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
