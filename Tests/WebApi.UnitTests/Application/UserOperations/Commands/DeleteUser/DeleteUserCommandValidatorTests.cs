using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.DeleteUser;

namespace Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    { 
        [Fact]
        public void WhenUserIdIsInvalid_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.UserId = 0;

            //act
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenAuthorIdIsValid_Validator_ShouldNotBeReturnError()
        {
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.UserId = 1;

            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}