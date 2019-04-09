using Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        User GetUser(string userName);
        User GetUser(string userName, string password);
        void SaveNewUser(User user);
    }
}
