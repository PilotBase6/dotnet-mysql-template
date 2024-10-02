

using Infrastructure.Entities;
using Infrastructure.Core;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly UserRepositorySql UserRepositorySql;
        private readonly ILogger<UserRepository> _logger;
                private readonly AppDbContext _context;

        public UserRepository(UserRepositorySql UserRepositorySql, ILogger<UserRepository> logger, AppDbContext context)
        {
            this.UserRepositorySql = UserRepositorySql;
            _logger = logger;
            _context = context;
        }

        public async Task<User> Create(User toCreate) => await UserRepositorySql.Create(toCreate);

        public async Task<User> Update(User toUpdate) => await UserRepositorySql.Update(toUpdate);

        public async Task<User> Delete(User toDelete) => await UserRepositorySql.Delete(toDelete);

        public async Task<User> GetById(Guid id) => await UserRepositorySql.GetById(id);

        public async Task<IEnumerable<User>> GetAll() => await UserRepositorySql.GetAll();

        public async Task<User> GetByEmailAsync(string email)
        {
            var userByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return userByEmail;
        }
    }


    public class UserRepositorySql : Repository<User>
    {
        private readonly  AppDbContext _context;
        public UserRepositorySql(AppDbContext context)
        {
            _context = context;

        }
        public override async Task<User> Create(User toCreate)
        {
            _context.Users.Add(toCreate);
            await _context.SaveChangesAsync();
            return toCreate;
        }

        public override async Task<User> Update(User toUpdate)
        {
            _context.Users.Update(toUpdate);
            await _context.SaveChangesAsync();
            return toUpdate;
        }

        public override async Task<User> Delete(User toDelete)
        {
            _context.Users.Remove(toDelete);
            await _context.SaveChangesAsync();
            return toDelete;
        }

        public override async Task<User> GetById(Guid id)
        {
            var getById = await  _context.Users.FindAsync(id);
            return getById;
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
    }
}