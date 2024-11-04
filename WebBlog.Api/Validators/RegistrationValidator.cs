using FluentValidation;
using WebBlog.Application.Dtos;

namespace WebBlog.Api.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationValidator() {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Missing required email field")
            .Matches(@"^.+@.+\.(com|net|org)$")
            .WithMessage("Invalid email field format: The email address must contain at least one character before and after the '@' symbol and end with an extension like .com, .net, .org.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Missing required password fields")
                .Matches(@"^(?=.*\d)(?=.*[A-Z]).{7,50}$")
                .WithMessage("Invalid password field format: The length of the string must be between 7 and 50 characters. It must contain at least one digit. It must contain at least one uppercase letter");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Missing required confirm_password fields")
                .Equal(x => x.Password)
                .WithMessage("Password and confirm password do not match");
        }
    }
}
