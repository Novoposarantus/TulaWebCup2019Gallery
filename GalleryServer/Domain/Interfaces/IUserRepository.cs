using Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> Users { get; }
        UserModel GetUser(int userId);
        UserModel GetUser(string userName);
        UserModel GetUser(string userName, string password);
        void SaveNewUser(UserModel user);
    }
}
