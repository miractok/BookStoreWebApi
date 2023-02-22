using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(0).NotEmpty();
            RuleFor(command => command.Model.Surname).MinimumLength(0).NotEmpty();
            RuleFor(command => command.Model.Email).MinimumLength(0).NotEmpty();
            RuleFor(command => command.Model.Password).MinimumLength(0).NotEmpty();
        }
    }
}