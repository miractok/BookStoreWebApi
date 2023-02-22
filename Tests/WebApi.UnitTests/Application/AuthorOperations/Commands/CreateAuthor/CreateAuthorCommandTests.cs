using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorNameSurnameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            var author = new Author() {NameSurname = "Test_WhenAlreadyExistAuthorNameSurnameIsGiven_InvalidOperationException_ShouldBeReturn", AuthorDateOfBirth = new System.DateTime(1990,01,10)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorModel() {  NameSurname = author.NameSurname };

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu yazar zaten mevcut.");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel() {NameSurname = "Adorno", AuthorDateOfBirth = DateTime.Now.Date};
            command.Model = model;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(author => author.NameSurname == model.NameSurname);
            author.Should().NotBeNull();
            author.AuthorDateOfBirth.Should().Be(model.AuthorDateOfBirth);
        }
    }
}