using FluentValidation;

namespace WebApi.Application.BookOperations.Command.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookID).GreaterThan(0);
            
        }
    }
}