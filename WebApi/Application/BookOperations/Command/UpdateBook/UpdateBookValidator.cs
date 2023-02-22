using FluentValidation;

namespace WebApi.Application.BookOperations.Command.UpdateBook
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(command => command.BookID).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0).LessThan(4);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.AuthorId).NotEmpty().GreaterThan(0);
        }
    }
}