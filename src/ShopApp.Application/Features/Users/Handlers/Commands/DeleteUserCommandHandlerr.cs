using MediatR;
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
            await _userRepository.DeleteUserAsync(request.Id, cancellationToken);
        }
    }
}
