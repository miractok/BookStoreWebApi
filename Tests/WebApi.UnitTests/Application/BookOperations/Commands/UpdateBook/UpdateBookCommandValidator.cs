using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Command.UpdateBook;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("",0,0)]
        [InlineData("",1,0)]
        [InlineData("",0,1)]
        [InlineData("deneme",0,0)]
        [InlineData(" ",0,0)]
        [InlineData(" ",1,0)]
        [InlineData(" ",0,1)]
        [InlineData(" ",1,1)]
        [InlineData("den",0,0)]
        [InlineData("den",1,0)]
        [InlineData("den",0,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int genreID, int authorId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateViewIdModel()
            {
                Title = title,
                GenreId = genreID,
                AuthorId = authorId
            };

            //act
            UpdateBookValidator validator = new UpdateBookValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var bookid =1;
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookID = bookid;
            command.Model = new UpdateViewIdModel()
            {
                Title = "denemelerdendenemeler",
                GenreId = 2,
                AuthorId = 3
            };

            UpdateBookValidator validator = new UpdateBookValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}