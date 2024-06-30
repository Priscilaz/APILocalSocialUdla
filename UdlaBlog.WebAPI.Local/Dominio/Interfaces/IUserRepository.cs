using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.WebAPI.Local.Domain.Models;

namespace UdlaBlog.WebAPI.Local.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string username);
    }
}
