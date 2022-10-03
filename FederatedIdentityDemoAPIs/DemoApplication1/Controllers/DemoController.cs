using DemoApplication1.Handlers;
using DemoApplication1.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize(Roles = "Admin,Developer,Basic")]
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
        [Authorize(Roles = "Admin,Developer,Basic")]
        [Route("anyroles")]
        public async Task<IActionResult> AnyAllowedAsync()
        {
            return Ok(await SendRequest("Hi {0} this endpoint allows any authenticated user. You are a {1}"));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Developer")]
        [Route("adminonly")]
        public async Task<IActionResult> AdminOnlyAsync()
        {
            return Ok(await SendRequest("Hi {0} this is Demo API 1, this endpoint allows any admin users. You are a {1}"));
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet]
        [Route("adminanddev")]
        public async Task<IActionResult> AdminOrDevAsync()
        {
            return Ok(await SendRequest("Hi {0} this is Demo API 1, this endpoint allows any admin or developer user. You are a {1}"));
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
