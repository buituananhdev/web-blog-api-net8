using System.Text.Json.Serialization;
using WebBlog.Domain.Enums;

namespace WebBog.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Fullname { get; set; } = null!;

        public string? Describe { get; set; }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Avatar { get; set; }

        public Status IsActive { get; set; } = Status.Inactive;
    }
}
