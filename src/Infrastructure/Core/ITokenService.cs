using Infrastructure.Entities;

    namespace Infrastructure.Core
{
    public interface ITokenService
    {
        string GenerateToken(User user);


    }

}