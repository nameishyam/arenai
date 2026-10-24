using FluentValidation;
using Server.Application.Features.Commands.Auth;

namespace Server.Application.Features.Validators.Auth
{
    public class SignupValidator : AbstractValidator<SignupCommand>
    {
        public SignupValidator()
        {
            RuleFor(x => x.SignupRequest.FirstName)
                .NotEmpty();

            RuleFor(x => x.SignupRequest.LastName)
            .NotEmpty();

            RuleFor(x => x.SignupRequest.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.SignupRequest.Password)
                .NotEmpty()
                .Matches(x => x.SignupRequest.ConfirmPassword);
        }
    }
}