using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Queries.GetUserDetails
{
    public class GetUserDetailQuery
    {
        public int UserId { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetUserDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserDetailViewModel Handle()
        {
            var user = _context.Users.SingleOrDefault(x=> x.Id == UserId);
            if(user == null)
                throw new InvalidOperationException("Kullanıcı bulunamadı.");
            
            return _mapper.Map<UserDetailViewModel>(user);
        }
    }

    public class UserDetailViewModel
        {
            public string? Name { get; set; }

            public string? Surname { get; set; }

            public string? Email { get; set; }

            public string? Password { get; set; }
        }
}