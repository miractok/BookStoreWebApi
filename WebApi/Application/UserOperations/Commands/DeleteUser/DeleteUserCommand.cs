using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        private readonly IBookStoreDbContext _context;

        public int UserId { get; set; }

        public DeleteUserCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x=> x.Id == UserId);
            if(user == null)
                throw new InvalidOperationException("Kullanıcı bulunamadı !");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}