using FederatedIdentityDemo.Models;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;

namespace AuthAPI.Handlers.Role
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, UserSummary>
    {
        public readonly IDistributedCache _cache;
        public GetRoleQueryHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public Task<UserSummary> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var username = request.User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();
            var role = request.User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault();

            if (role == null || username == null)
            {
                throw new Exception($"One or more claims are null for an authenticated user! Username: {username}, Role: {role}");
            }

            return Task.FromResult(new UserSummary() { username = username.Value, role = role.Value });
        }
    }
}
