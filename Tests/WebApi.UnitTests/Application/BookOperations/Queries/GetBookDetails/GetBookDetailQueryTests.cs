using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetails;
using WebApi.DBOperations;

namespace Application.BookOperations.Queries.GetBookDetails
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookIdIsWrongGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookID = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Girdiğiniz Id hiçbir kitapla eşleşmemektedir.");
        }

        [Fact]
        public void WhenBookIdIsValidGiven_Book_ShouldBeReturn()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookID = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Id == 1);
            book.Should().NotBeNull();
        }
    }
}