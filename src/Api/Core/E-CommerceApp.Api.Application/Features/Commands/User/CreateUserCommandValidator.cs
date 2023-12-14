using E_CommerceApp.Common.Models.RequestModels.User;
using FluentValidation;
using FluentValidation.Validators;

namespace E_CommerceApp.Api.Application.Features.Commands.User;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(e => e.Email)
            .NotNull()
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("{PropertyName} is not valid email address!");
        RuleFor(e => e.Password)
            .NotNull()
            .MinimumLength(6)
            .WithMessage("{PropertyName} should be minimum 6 character!");

    }
}