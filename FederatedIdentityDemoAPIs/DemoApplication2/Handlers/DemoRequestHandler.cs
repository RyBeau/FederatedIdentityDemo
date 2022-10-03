using DemoApplication2.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;

namespace DemoApplication2.Handlers
{
    public class DemoRequestHandler : IRequestHandler<DemoRequest, DemoResponse>
    {
        public readonly IDistributedCache _cache;

        public DemoRequestHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public Task<DemoResponse> Handle(DemoRequest request, CancellationToken cancellationToken)
        {
            var username = request.User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();
            var role = request.User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault();

            if (role == null || username == null)
            {
                throw new Exception($"One or more claims are null for an authenticated user! Username: {username}, Role: {role}");
            }

            return Task.FromResult(new DemoResponse()
            {
                Content = string.Format(request.Message, username.Value, role.Value)
            });
        }
    }
}
