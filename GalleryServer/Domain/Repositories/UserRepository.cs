using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    return context.Users.AsQueryable();
                }
            }
        }

        public async Task<User> GetUser(string userName)
        {
            return await Users.FirstOrDefaultAsync(user => user.UserName == userName);
        }

        public Task SaveNewUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
