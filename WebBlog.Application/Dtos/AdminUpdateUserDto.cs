using System.Text.Json.Serialization;

namespace WebBlog.Application.Dtos
{
    public class AdminUpdateUserDto
    {
        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        [JsonPropertyName("role_id")]
        public int RoleId { get; set; }
    }
}
