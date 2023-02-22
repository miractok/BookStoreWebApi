using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;

        public int AuthorId { get; set; }

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if(author == null)
                throw new InvalidOperationException("Aradığınız yazar bulunamadı.");

            var authorsbook = _context.Books.Where(x=> x.AuthorId == author.Id).Any(); 
            if (authorsbook) 
                throw new InvalidProgramException("Yazarın kitabı yayınlanmaktadır. Yazarı silmeden önce kitabı yayından kaldırınız !");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}