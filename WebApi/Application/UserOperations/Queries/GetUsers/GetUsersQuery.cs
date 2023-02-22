using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Queries.GetUsers
{
    public class GetUsersQuery
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetUsersQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<UsersViewModel> Handle()
        {
            var users = _context.Users.OrderBy(x=> x.Id);
            List<UsersViewModel> returnObj = _mapper.Map<List<UsersViewModel>>(users);
            return returnObj;
        }
    }


    public class UsersViewModel
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}