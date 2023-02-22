using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.UpdateUser;

namespace Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("","","","")]
        [InlineData(" "," "," "," ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, string email, string password)
        {
            //arrange
            UpdateUserCommand command = new UpdateUserCommand(null);
            command.Model = new UpdateUserModel()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password
            };

            //act
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            var userid =1;
            UpdateUserCommand command = new UpdateUserCommand(null);
            command.UserId = userid;
            command.Model = new UpdateUserModel()
            {
                Name = "ismail",
                Surname = "ismailoğlu",
                Email = "ismail@gmail.com",
                Password = "123456"
            };

            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}