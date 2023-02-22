using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAuthorIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Aradığınız yazar bulunamadı.");
        }

        // [Fact]
        // public void WhenGivenBookIdNotEqualAuthorId_InvalidOperationException_ShouldBeReturn()
        // {
        //     DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        //     command.AuthorId = 1;

        //    FluentActions
        //         .Invoking(() => command.Handle())
        //         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazarın kitapları mevcut olduğundan silinemez ...");
        // }

        [Fact]

        public void WhenAuthorIdIsValid_Genre_ShouldBeDeleted()
        {
            var author = new Author() {NameSurname = "Test_WhenAuthorIdIsValid_InvalidOperationException_ShouldBeReturn", AuthorDateOfBirth = new System.DateTime(1990,01,10)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);    
            command.AuthorId = author.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var authorCheck = _context.Users.SingleOrDefault(authorCheck=> authorCheck.Id == author.Id);
            authorCheck.Should().BeNull();
        }
    }
}