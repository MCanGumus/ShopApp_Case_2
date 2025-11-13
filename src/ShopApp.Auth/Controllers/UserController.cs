using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Features.Users.Commands;
using ShopApp.Application.Features.Users.Queries;

namespace ShopApp.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery command)
        {
            var authResult = await _mediator.Send(command);
            if (authResult == null)
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");

            return Ok(authResult);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenQuery command)
        {
            var newAuthResult = await _mediator.Send(command);
            if (newAuthResult == null)
                return Unauthorized("Refresh token geçersiz veya süresi dolmuş.");

            return Ok(newAuthResult);
        }
    }
}
