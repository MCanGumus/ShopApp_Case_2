using MediatR;
using ShopApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Users.Queries
{
    public record RefreshTokenQuery(string refreshToken) : IRequest<AuthResult>;
}
