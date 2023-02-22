using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;

namespace Application.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 0;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldGetDetail()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 1;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().Be(0);
        }
    }
}