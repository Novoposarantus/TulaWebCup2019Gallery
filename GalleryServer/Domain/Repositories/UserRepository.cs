using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }
        public IQueryable<User> Users
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Users.Include(user=>user.Role).AsQueryable();
                }
            }
        }

        public async Task<User> GetUser(string userName)
        {
            return await Users.FirstOrDefaultAsync(user => user.UserName == userName);
        }

        public async Task SaveNewUser(User user)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
