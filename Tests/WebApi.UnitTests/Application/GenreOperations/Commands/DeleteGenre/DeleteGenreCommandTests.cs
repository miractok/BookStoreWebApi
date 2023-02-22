using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGenreIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Kitap türü bulunamadı.");
        }
        [Fact]
        public void WhenGenreIdIsValid_Genre_ShouldBeDeleted()
        {
            var genre = new Genre() {Name = "Test_WhenGenreIdIsValid_InvalidOperationException_ShouldBeReturn"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);    
            command.GenreId = genre.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var genreCheck = _context.Genres.SingleOrDefault(genreCheck=> genreCheck.Id == genre.Id);
            genreCheck.Should().BeNull();
        }
    }
}