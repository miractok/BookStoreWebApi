using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Queries.GetAuthorDetails
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorIdIsWrongGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Yazar bulunamadı.");
        }

        [Fact]
        public void WhenAuthorIdIsValidGiven_Book_ShouldBeReturn()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Id == 1);
            author.Should().NotBeNull();
        }
    }
}