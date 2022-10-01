using FederatedIdentityDemo.Models;
using MediatR;
using System.Security.Claims;

namespace AuthAPI.Handlers.ValidateSession
{
    public class ValidateSessionQuery : IRequest<UserSummary>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
