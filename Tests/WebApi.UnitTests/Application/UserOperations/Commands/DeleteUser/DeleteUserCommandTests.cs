using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.DeleteUser;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteUserCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenUserIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            DeleteUserCommand command = new DeleteUserCommand(_context);

            FluentActions.Invoking(() => command.Handle()) .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı bulunamadı !");
        }

        [Fact]

        public void WhenUserIdIsValid_Genre_ShouldBeDeleted()
        {
            var user = new User() {Name = "Test_WhenAlreadyExistUserNameIsGiven_InvalidOperationException_ShouldBeReturn", Surname = "Test_WhenAlreadyExistUserSurnameIsGiven_InvalidOperationException_ShouldBeReturn", Email = "Test_WhenAlreadyExistUserEmailIsGiven_InvalidOperationException_ShouldBeReturn", Password = "Test_WhenAlreadyExistUserPasswordIsGiven_InvalidOperationException_ShouldBeReturn"};
            _context.Users.Add(user);
            _context.SaveChanges();

            DeleteUserCommand command = new DeleteUserCommand(_context);    
            command.UserId = user.Id;


            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var userCheck = _context.Users.SingleOrDefault(userCheck=> userCheck.Id == user.Id);
            userCheck.Should().BeNull();
        }
    }
}