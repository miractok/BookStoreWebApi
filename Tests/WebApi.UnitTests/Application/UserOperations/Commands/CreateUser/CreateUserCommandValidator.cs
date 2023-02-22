using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData("","","","")]
        [InlineData(" "," "," "," ")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, string email, string password)
        {
            //arrange
            CreateUserCommand command = new CreateUserCommand(null,null);
            command.Model = new CreateUserModel()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password
            };

            //act
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateUserCommand command = new CreateUserCommand(null,null);
            command.Model = new CreateUserModel()
            {
                Name = "İsmail",
                Surname = "İsmailoğlu",
                Email = "ismail@hotmail.com",
                Password = "123456789123456"
            };

            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}