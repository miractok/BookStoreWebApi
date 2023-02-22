using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateUserCommandValidator : AbstractValidator<CreateTokenCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Email).NotEmpty().MinimumLength(0);
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(0);
        }
    }
}