using DemoApplication2.Responses;
using MediatR;
using System.Security.Claims;

namespace DemoApplication2.Handlers
{
    public class DemoRequest : IRequest<DemoResponse>
    {
        public ClaimsPrincipal User { get; set; }
        public string Message { get; set; }
    }
}
