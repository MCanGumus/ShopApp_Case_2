using MediatR;
using Serilog;
using ShopApp.Application.Features.Users.Commands;
using ShopApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Users.Handlers.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

            if (existingUser == null)
            {
                Log.Warning("User with Id {UserId} not found", request.Id);
                throw new KeyNotFoundException("User not found");
            }

            await _userRepository.DeleteUserAsync(request.Id, cancellationToken);

            Log.Information("User with Id {UserId} deleted successfully", request.Id);
        }
    }
}
