using ShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
        Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);
        Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
        Task<User> GetUserByUsername(string username, CancellationToken cancellationToken);
    }
}
