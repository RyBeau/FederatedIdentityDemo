using DemoApplication2.Handlers;
using DemoApplication2.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication2.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize(Roles = "Admin,Developer")]
    public class DemoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DemoController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("authorize")]
        public IActionResult CheckAuth()
        {
            return Ok("You can access it");
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> AllAllowedAsync()
        {
            return Ok(await SendRequest("Hi {0} this is Demo API 2, this endpoint allows any admin or developer users. You are a {1}"));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("adminonly")]
        public async Task<IActionResult> AdminOnlyAsync()
        {
            return Ok(await SendRequest("Hi {0} this is Demo API 2, this endpoint allows any admin users. You are a {1}"));
        }

        [Authorize(Roles = "Developer")]
        [HttpGet]
        [Route("developer")]
        public async Task<IActionResult> DevOnlyAsync()
        {
            return Ok(await SendRequest("Hi {0} this is Demo API 2, this endpoint allows any developer user. You are a {1}"));
        }

        private Task<DemoResponse> SendRequest(string message)
        {
            var request = new DemoRequest()
            {
                Message = message,
                User = User
            };

            return _mediator.Send(request);
        }
    }
}
