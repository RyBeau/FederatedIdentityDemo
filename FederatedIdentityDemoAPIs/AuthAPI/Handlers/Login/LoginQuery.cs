using FederatedIdentityDemo.Models;
using MediatR;

namespace AuthAPI.Handlers.Login
{
    public class LoginQuery : IRequest<UserSummary>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SessionId { get; set; }
    }
}
