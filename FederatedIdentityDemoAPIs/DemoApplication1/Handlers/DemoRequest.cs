using DemoApplication1.Responses;
using MediatR;
using System.Security.Claims;

namespace DemoApplication1.Handlers
{
    public class DemoRequest : IRequest<DemoResponse>
    {
        public ClaimsPrincipal User { get; set; }
        public string Message { get; set; }
    }
}
