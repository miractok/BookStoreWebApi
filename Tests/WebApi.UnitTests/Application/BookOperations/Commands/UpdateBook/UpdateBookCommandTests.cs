using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Command.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidIdGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            var book = new Book() {Title = "Test_WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new System.DateTime(1990,01,10), GenreId = 1, AuthorId = 1};
            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Model = new UpdateViewIdModel() { Title = book.Title };

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Girdiğiniz Id hiçbir kitapla eşleşmemektedir.");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateViewIdModel model = new UpdateViewIdModel() {Title="Hobbit", GenreId = 1, AuthorId = 1};
            command.Model = model;
            command.BookID = 1;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updatebook = _context.Books.SingleOrDefault(book => book.Title == model.Title);
            updatebook.Should().NotBeNull();
            updatebook.GenreId.Should().Be(model.GenreId);
            updatebook.AuthorId.Should().Be(model.AuthorId);
        }
    }
}