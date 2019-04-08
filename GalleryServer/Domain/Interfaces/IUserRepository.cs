using Models;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        Task<User> GetUser(string userName);
        Task SaveNewUser(User user);
    }
}
