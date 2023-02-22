using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var author = _context.Authors.Where(x=> x.IsActive).OrderBy(x=> x.Id);
            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(author);
            return returnObj;
        }
    }

    public class AuthorsViewModel
    {
        public int Id { get; set; }

        public string? NameSurname { get; set; }

        public DateTime AuthorDateOfBirth { get; set; }
    }
}