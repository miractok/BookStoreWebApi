using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistUserInputIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            var user = new User() {Name = "Murat", Surname = "Muhlama", Email = "murat@gmail.com", Password = "456132456132"};
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateUserCommand command = new CreateUserCommand(_context,_mapper);
            command.Model = new CreateUserModel() {  Email = user.Email };

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı zaten mevcut.");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            CreateUserModel model = new CreateUserModel() {Name = "ismail", Surname = "ismailoğlu", Email = "ismail@hotmail.com", Password = "123456789132456"};
            command.Model = model;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var user = _context.Users.SingleOrDefault(user => user.Email == model.Email);
            user.Should().NotBeNull();
            user.Name.Should().Be(model.Name);
            user.Surname.Should().Be(model.Surname);
            user.Password.Should().Be(model.Password);
        }
    }
}