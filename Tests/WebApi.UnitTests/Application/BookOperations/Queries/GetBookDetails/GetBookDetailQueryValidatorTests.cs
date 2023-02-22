using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetails;

namespace Application.BookOperations.Queries.GetBookDetails
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookID = 0;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookID = 1;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}