using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Domain.Entities;

namespace UdlaBlog.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(string username);
    }
}
