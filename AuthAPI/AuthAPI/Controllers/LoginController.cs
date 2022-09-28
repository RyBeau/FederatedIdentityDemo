using AuthAPI.Handlers.Login;
using AuthAPI.Handlers.Logout;
using AuthAPI.Handlers.ValidateSession;
using AuthAPI.Services.CookieHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using System.Net;

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
        public async Task<IActionResult> Login ([FromBody] LoginRequest loginRequest)
        {
            var request = new LoginQuery()
            {
                Username = loginRequest.Username,
                Password = loginRequest.Password,
                SessionId = GenerateSessionId()
            };

            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result).AddCookie(Response, "session_id", request.SessionId);
            }

            return NotFound();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout ()
        {
            var sessionId = GetSessionId();

            if (sessionId != null)
            {
                var request = new LogoutQuery
                {
                    SessionId = sessionId
                };

                await _mediator.Send(request);

                return Ok().RemoveCookie(Response, "session_id");
            }
            return Unauthorized();
        }


        [HttpGet("authenticate")]
        public async Task<IActionResult> ValidateSession()
        {
            var sessionId = GetSessionId();

            if (sessionId != null)
            {
                var request = new ValidateSessionQuery()
                {
                    SessionId = sessionId
                };
                var result = await _mediator.Send(request);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            return Unauthorized();
        }

        private string GenerateSessionId()
        {
            return Guid.NewGuid().ToString();
        }

        private string? GetSessionId()
        {
            return Request.Cookies["session_id"];
        }
    }
}
