using WebBog.Application.Dtos;
using FluentValidation;

namespace WebBog.Api.Validators
{
    public class TokenObtainPairValidator : AbstractValidator<TokenObtainPairDto>
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
