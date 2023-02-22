using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommand
    {
        private readonly IBookStoreDbContext _context;

        public int UserId { get; set; }
        public UpdateUserModel Model { get; set; }

        public UpdateUserCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == UserId);
            if(user == null)
                throw new InvalidOperationException("Aradığınız kullanıcı bulunamadı.");

            if(_context.Users.Any(x => x.Email.ToLower() == Model.Email.ToLower() && x.Id != UserId))
                throw new InvalidOperationException("Kullanıcı zaten mevcut.");

            //author.NameSurname = string.IsNullOrEmpty(Model.NameSurname.Trim()) ? author.NameSurname : Model.NameSurname;

            user.Name = Model.Name != default ? Model.Name : user.Name;
            user.Surname = Model.Surname != default ? Model.Surname : user.Surname;
            user.Email = Model.Email != default ? Model.Email : user.Email;
            user.Password = Model.Password != default ? Model.Password : user.Password;
            
            _context.SaveChanges();
        }
    }

    public class UpdateUserModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}