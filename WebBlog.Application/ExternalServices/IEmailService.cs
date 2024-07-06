using WebBog.Application.Dtos;

namespace WebBog.Application.ExternalServices;

public interface IEmailService
{
    Task SendMailAsync(MailDataDto mailData);
    Task SendEmailForgotPassword(string email);
}