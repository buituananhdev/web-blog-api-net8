using WebBlog.Application.Dtos;

namespace WebBlog.Application.ExternalServices
{
    public interface IRabbitMQService
    {
        Task SendEmailAsync(MailDataDto mailData);
    }
}
