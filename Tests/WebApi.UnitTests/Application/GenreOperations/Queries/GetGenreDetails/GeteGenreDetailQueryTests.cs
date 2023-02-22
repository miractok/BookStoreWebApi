using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.DBOperations;

namespace Application.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGenreIdIsWrongGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Kitap türü bulunamadi");
        }

        [Fact]
        public void WhenGenreIdIsValidGiven_Book_ShouldBeReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == 1);
            genre.Should().NotBeNull();
        }
    }
}