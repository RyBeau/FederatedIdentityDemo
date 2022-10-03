using FederatedIdentityDemo.Models;
using MediatR;
using System.Security.Claims;

namespace AuthAPI.Handlers.Role
{
    public class GetRoleQuery : IRequest<UserSummary>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
