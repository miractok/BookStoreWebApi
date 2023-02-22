using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Command.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenBookIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Girdiğiniz Id hiçbir kitapla eşleşmemektedir.");
        }
        [Fact]
        public void WhenBookIdIsValid_Book_ShouldBeDeleted()
        {
            var book = new Book() {Title = "Test_WhenBookIdIsInDataBase_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new System.DateTime(1990,01,10), GenreId = 1, AuthorId = 1};
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);    
            command.BookID = book.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var bookCheck = _context.Books.SingleOrDefault(bookCheck=> bookCheck.Id == book.Id);
            bookCheck.Should().BeNull();
        }
    }
}