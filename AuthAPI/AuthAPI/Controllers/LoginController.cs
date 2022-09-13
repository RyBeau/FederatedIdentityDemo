using AuthAPI.Handlers.Login;
using AuthAPI.Handlers.Logout;
using AuthAPI.Handlers.ValidateSession;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> Login ([FromBody] LoginQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout ()
        {
            var request = new LogoutQuery
            {
                SessionId = "Test ID"
            };
            return Ok(await _mediator.Send(request));
        }


        [HttpGet("authenticate")]
        public async Task<IActionResult> ValidateSession([FromQuery] ValidateSessionQuery request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
