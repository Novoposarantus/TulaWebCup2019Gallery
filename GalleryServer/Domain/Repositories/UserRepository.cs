using Domain.Helpers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Exceptions;
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
            return await Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<User> GetUser(string userName, string password)
        {
            password = AuthenticationHelper.HashPassword(password);
            return await Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
        }

        public async Task SaveNewUser(User user)
        {
            if(await GetUser(user.UserName) != null)
            {
                throw new RegistrationException("Пользователь с таким именем уже зарегестрирован");
            }
            user.Password = AuthenticationHelper.HashPassword(user.Password);
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
