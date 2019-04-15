using Domain.Helpers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Exceptions;
using System.Linq;
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
                    return context.Users.Include(user => user.Role);
                }
            }
        }

        public UserModel GetUser(int userId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var user = context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    throw new UserRepositoryException("Пользователь с таким логином не найден");
                }
                return user;
            }
        }

        public UserModel GetUser(string userName)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var user = context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserName == userName);
                if (user == null)
                {
                    throw new UserRepositoryException("Пользователь с таким логином не найден");
                }
                return user;
            }
        }

        public UserModel GetUser(string userName, string password)
        {
            password = AuthenticationHelper.HashPassword(password);
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var user = context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserName == userName && u.Password == password);
                if(user == null)
                {
                    throw new UserRepositoryException("Неверный логин или пароль");
                }
                return user;
            }
        }

        public void SaveNewUser(UserModel user)
        {
            try
            {
                GetUser(user.UserName);
                throw new UserRepositoryException("Пользователь с таким именем уже зарегестрирован");
            }
            catch (UserRepositoryException) { }
            user.Password = AuthenticationHelper.HashPassword(user.Password);
            user.RoleId = 1;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                user.Id = 0;
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
