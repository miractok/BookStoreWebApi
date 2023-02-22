using FluentValidation;

namespace WebApi.Application.UserOperations.Queries.GetUserDetails
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
        }
    }
}