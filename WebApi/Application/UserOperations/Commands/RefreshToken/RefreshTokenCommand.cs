using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string? RefreshToken { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IConfiguration _configuraiton;
        public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuraiton)
        {
            _dbContext = dbContext;
            _configuraiton = configuraiton;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if(user != null)
            {
                //Token yarat
                TokenHandler handler = new TokenHandler(_configuraiton);
                Token token = handler.CreateAccesToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                
                return token;
            }
            else
                throw new InvalidOperationException("Geçerli bir Refresh Token bulunamadı !");
        }
    }
}