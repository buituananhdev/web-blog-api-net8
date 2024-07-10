using System.ComponentModel.DataAnnotations;
namespace WebBog.Application.Dtos
{
    public class LoginDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
