using MediatR;
using Serilog;
using ShopApp.Application.Features.Users.Commands;
using ShopApp.Core.Entities;
using ShopApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Users.Handlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        public CreateUserCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUsername = await _repository.GetUserByUsername(request.Username, cancellationToken);

            if (existingUsername != null)
            {
                Log.Warning("Username {Username} already exists", request.Username);
                throw new InvalidOperationException("Username already exists");
            }

            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
            };

            var addedUser = await _repository.AddUserAsync(user, cancellationToken);

            Log.Information("User {Username} registered successfully", request.Username);

            return addedUser;
        }
    }
}
