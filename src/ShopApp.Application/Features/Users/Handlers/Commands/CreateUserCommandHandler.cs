using MediatR;
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
        public Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
            };

            var addedUser = _repository.AddUserAsync(user, cancellationToken);

            return addedUser;
        }
    }
}
