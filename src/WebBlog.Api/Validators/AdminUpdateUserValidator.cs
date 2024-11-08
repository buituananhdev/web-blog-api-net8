﻿using WebBlog.Application.Dtos;
using FluentValidation;

namespace WebBlog.Api.Validators
{
    public class AdminUpdateUserValidator : AbstractValidator<AdminUpdateUserDto>
    {
        public AdminUpdateUserValidator() {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Missing required full_name field")
                .Matches(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z])*$")
                .WithMessage("Invalid full_name field format: Starts with one or more letters,  allows for characters like spaces,hyphens, apostrophes, commas, and periods within the name, and does not allow trailing spaces");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Missing required email field")
                .Matches(@"^.+@.+\.(com|net|org)$")
                .WithMessage("Invalid email field format: The email address must contain at least one character before and after the '@' symbol and end with an extension like .com, .net, .org.");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Missing required role_id field");
        }
    }
}
