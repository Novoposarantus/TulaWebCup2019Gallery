using Domain.Helpers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }
        public IEnumerable<UserModel> Users
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Users.Include(user=>user.Role).ToList();
                }
            }
        }

        public UserModel GetUser(string userName)
        {
            return Users.FirstOrDefault(u => u.UserName == userName);
        }
        public UserModel GetUser(string userName, string password)
        {
            password = AuthenticationHelper.HashPassword(password);
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Users.Include(user => user.Role).FirstOrDefault(u => u.UserName == userName && u.Password == password);
            }
        }

        public void SaveNewUser(UserModel user)
        {
            if(GetUser(user.UserName) != null)
            {
                throw new RegistrationException("Пользователь с таким именем уже зарегестрирован");
            }
            user.Password = AuthenticationHelper.HashPassword(user.Password);
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
