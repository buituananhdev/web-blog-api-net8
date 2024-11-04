using WebBlog.Application.Dtos;
using FluentValidation;

namespace WebBlog.Api.Validators
{
    public class TokenObtainPairValidator : AbstractValidator<LoginDto>
    {
        public TokenObtainPairValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("The email field is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The password field is required");
        }
    }
}
