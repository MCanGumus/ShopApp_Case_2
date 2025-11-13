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
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, AuthResult>
    {
        private readonly IJwtService _jwtService;
        private readonly ICacheService _cacheService;
        private readonly IUserRepository _userRepository;

        public RefreshTokenQueryHandler(
            IJwtService jwtService,
            ICacheService cacheService,
            IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _cacheService = cacheService;
            _userRepository = userRepository;
        }

        public async Task<AuthResult> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshKey = $"refreshToken:{request.refreshToken}";

            var userIdStr = await _cacheService.GetAsync<string>(refreshKey);

            if (string.IsNullOrEmpty(userIdStr))
                throw new UnauthorizedAccessException("Invalid refresh token");

            await _cacheService.RemoveAsync(refreshKey);

            var user = await _userRepository.GetUserByIdAsync(Guid.Parse(userIdStr), cancellationToken);

            var newAccessToken = _jwtService.GenerateToken(user);
            var newRefreshToken = Guid.NewGuid().ToString("N");
            var newRefreshKey = $"refreshToken:{newRefreshToken}";
            await _cacheService.SetAsync(newRefreshKey, user.Id.ToString(), TimeSpan.FromDays(7));

            return new AuthResult
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
