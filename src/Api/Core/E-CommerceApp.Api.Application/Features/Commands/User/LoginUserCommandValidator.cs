using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Common.Models.RequestModels.User;
using FluentValidation;
using FluentValidation.Validators;

namespace E_CommerceApp.Api.Application.Features.Commands.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(e => e.EmailAddress)
                .NotNull()
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} is not valid email address!");
            RuleFor(e => e.Password)
                .NotNull()
                .MinimumLength(6)
                .WithMessage("{PropertyName} should be minimum 6 character!");
        }
    }
}
