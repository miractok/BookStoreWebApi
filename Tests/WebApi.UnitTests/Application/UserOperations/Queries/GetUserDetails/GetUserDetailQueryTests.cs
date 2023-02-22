using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Queries.GetUserDetails;
using WebApi.DBOperations;

namespace Application.UserOperations.Queries.GetUserDetails
{
    public class GetUserDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public GetUserDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookIdIsWrongGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
            query.UserId = -1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Kullanıcı bulunamadı.");
        }

        [Fact]
        public void WhenBookIdIsValidGiven_Book_ShouldBeReturn()
        {
            GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
            query.UserId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var user = _context.Books.SingleOrDefault(user => user.Id == 1);
            user.Should().NotBeNull();
        }
    }
}