using WebBlog.Application.Dtos;

namespace WebBlog.Application.ExternalServices;

public interface IEmailService
{
    Task SendMailAsync(MailDataDto mailData);
    Task SendEmailForgotPassword(string email);
}