using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Command.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookID { get; set; }
        public UpdateViewIdModel Model { get; set; }
        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id == BookID);
            if(book == null)
                throw new InvalidOperationException("Girdiğiniz Id hiçbir kitapla eşleşmemektedir.");

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
            

            _dbContext.SaveChanges();
        }
    }

    public class UpdateViewIdModel
    {
        public string? Title { get; set; }

        public int GenreId { get; set; }

        public int AuthorId { get; set; }
    }
}