using System.ComponentModel.DataAnnotations;
namespace WebBlog.Application.Dtos
{
    public class LoginDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
