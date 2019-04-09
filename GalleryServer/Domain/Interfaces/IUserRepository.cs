using Models.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        Task<User> GetUser(string userName);
        Task<User> GetUser(string userName, string password);
        Task SaveNewUser(User user);
    }
}
