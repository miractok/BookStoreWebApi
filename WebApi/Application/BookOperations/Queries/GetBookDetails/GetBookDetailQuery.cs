using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetails
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookID { get; set; }
        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BooksViewIdModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).Where(book=> book.Id == BookID).SingleOrDefault();
            if(book == null)
                throw new InvalidOperationException("Girdiğiniz Id hiçbir kitapla eşleşmemektedir.");
            BooksViewIdModel vm = _mapper.Map<BooksViewIdModel>(book); 
            return vm;
        }
    }

    public class BooksViewIdModel
    {
        public string? Title { get; set; }

        public string? Genre { get; set; }

        public int PageCount { get; set; }

        public string? PublishDate { get; set; }
        public string? Author { get; set; }
    }
}