using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Queries.GetUserDetails;

namespace Application.UserOperations.Queries.GetUserDetails
{
    public class GetUserDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetUserDetailQuery query = new GetUserDetailQuery(null, null);
            query.UserId = 0;
            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetUserDetailQuery query = new GetUserDetailQuery(null, null);
            query.UserId = 1;
            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}