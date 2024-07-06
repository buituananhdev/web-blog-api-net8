using System.ComponentModel.DataAnnotations;
namespace WebBog.Application.Dtos
{
    public class TokenObtainPairDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
