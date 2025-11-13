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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);
            if (userToUpdate == null)
            {
                throw new Exception("User not found");
            }

            userToUpdate.Username = request.Username;
            userToUpdate.Email = request.Email;
            userToUpdate.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _userRepository.UpdateUserAsync(userToUpdate, cancellationToken);

            return updatedUser;
        }
    }
}
