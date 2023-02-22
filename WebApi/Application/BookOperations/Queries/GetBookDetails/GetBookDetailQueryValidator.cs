using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookDetails
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query => query.BookID).GreaterThan(0);
        }
    }
}