using AuthAPI.Handlers.Login;
using AuthAPI.Handlers.Logout;
using AuthAPI.Handlers.Role;
using AuthAPI.Requests;
using FederatedIdentityDemo.Shared.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class LoginController : Controller
    {
        private IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var request = new LoginQuery()
            {
                Username = loginRequest.Username,
                Password = loginRequest.Password,
                SessionId = CacheAuthHelper.GenerateSessionId()
            };

            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result).AddCookie(Response, CacheAuthConstants.CookieName, request.SessionId);
            }

            return NotFound();
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            var sessionId = CacheAuthHelper.GetSessionId(Request);

            if (sessionId != null)
            {
                var request = new LogoutQuery
                {
                    SessionId = sessionId
                };

                await _mediator.Send(request);

                return Ok().RemoveCookie(Response, CacheAuthConstants.CookieName);
            }
            return Unauthorized();
        }


        [HttpGet("role")]
        [Authorize]
        public async Task<IActionResult> GetRoleAsync()
        {

            var request = new GetRoleQuery()
            {
                User = User,
            };
            var result = await _mediator.Send(request);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }
    }
}
