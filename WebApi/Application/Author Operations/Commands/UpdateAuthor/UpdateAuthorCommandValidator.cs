using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.NameSurname).MinimumLength(4).NotEmpty();
            RuleFor(command=> command.Model.AuthorDateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}