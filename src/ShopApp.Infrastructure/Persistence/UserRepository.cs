using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Entities;
using ShopApp.Core.Interfaces;
using ShopApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApp.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);

            return user;
        }
        public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            user.PasswordHash = "";

            return user;
        }
        public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            //user.IsActive = false;
            //_context.Users.Update(user);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> GetUserByUsername(string username, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

            return user;
        }
    }
}
