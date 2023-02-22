using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.NameSurname).NotEmpty().MinimumLength(1);
            RuleFor(command=> command.Model.AuthorDateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}