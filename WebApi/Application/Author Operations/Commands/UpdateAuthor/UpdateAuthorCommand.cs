using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;

        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }

        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author == null)
                throw new InvalidOperationException("Aradığınız yazar bulunamadı.");

            if(_context.Authors.Any(x => x.NameSurname.ToLower() == Model.NameSurname.ToLower() && x.Id != AuthorId))
                throw new InvalidOperationException("Yazar zaten mevcut.");

            //author.NameSurname = string.IsNullOrEmpty(Model.NameSurname.Trim()) ? author.NameSurname : Model.NameSurname;

            author.NameSurname = Model.NameSurname != default ? Model.NameSurname : author.NameSurname;
            author.AuthorDateOfBirth = Model.AuthorDateOfBirth != default ? Model.AuthorDateOfBirth : author.AuthorDateOfBirth;

            author.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string? NameSurname { get; set; }

        public DateTime AuthorDateOfBirth { get; set; }

        public bool IsActive { get; set; } = true;
    }
}