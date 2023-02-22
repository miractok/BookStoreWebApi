using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateUserCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidIdGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            var user = new User() {Name = "Test_WhenAlreadyExistUserNameIsGiven_InvalidOperationException_ShouldBeReturn", Surname = "Test_WhenAlreadyExistUserSurnameIsGiven_InvalidOperationException_ShouldBeReturn", Email = "Test_WhenAlreadyExistUserEmailIsGiven_InvalidOperationException_ShouldBeReturn", Password = "Test_WhenAlreadyExistUserPasswordIsGiven_InvalidOperationException_ShouldBeReturn"};
            _context.Users.Add(user);
            _context.SaveChanges();

            UpdateUserCommand command = new UpdateUserCommand(_context);
            command.Model = new UpdateUserModel() { Email = user.Email };
            command.UserId= 895;

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aradığınız kullanıcı bulunamadı.");
            
        }

        [Fact]
        public void WhenAlreadyExistUserEmailIsGiven_InvalidOperationException_ShouldBeReturn()
        {

            //arrange (Hazırlık)
            var user = new User() {Name = "mehmet", Surname = "mehmetoğlu", Email = "mehmet@gmail.com", Password = "123456132"};
            _context.Users.Add(user);
            _context.SaveChanges();

            UpdateUserCommand command = new UpdateUserCommand(_context);
            command.UserId = 1;
            command.Model = new UpdateUserModel() { Email = "mehmet@gmail.com"};

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı zaten mevcut.");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //arrange
            var user = new User() {Email = "hikmet@gmail.com"};
            _context.Users.Add(user);
            _context.SaveChanges();
            
            UpdateUserCommand command = new UpdateUserCommand(_context);
            UpdateUserModel model = new UpdateUserModel() {Email="hikmet@gmail.com"};
            command.Model = model;
            command.UserId = user.Id;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updateuser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
            updateuser.Should().NotBeNull();
        }
    }
}