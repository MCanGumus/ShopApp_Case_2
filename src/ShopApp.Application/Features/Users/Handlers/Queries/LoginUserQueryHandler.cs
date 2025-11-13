using MediatR;
using ShopApp.Application.Dtos;
using ShopApp.Application.Features.Users.Queries;
using ShopApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Users.Handlers.Queries
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ICacheService _cacheService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUserQueryHandler(
            IUserRepository userRepository,
            IJwtService jwtService,
            ICacheService cacheService,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _cacheService = cacheService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsername(request.Username, cancellationToken);

            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            // Access token
            var accessToken = _jwtService.GenerateToken(user);

            // Refresh token
            var refreshToken = Guid.NewGuid().ToString("N"); // 32 byte hex string
            var refreshKey = $"refreshToken:{refreshToken}";
            var ttl = TimeSpan.FromDays(7); 

            await _cacheService.SetAsync(refreshKey, user.Id.ToString(), ttl);

            return new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
