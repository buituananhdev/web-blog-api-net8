using WebBlog.Application.Dtos;

namespace WebBlog.Application.ExternalServices
{
    public interface IRabbitMQService
    {
        void SendEmailAsync(MailDataDto mailData);
    }
}
