using Infrastructure.Entities;

namespace Infrastructure.Core
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid userId);
    }
}