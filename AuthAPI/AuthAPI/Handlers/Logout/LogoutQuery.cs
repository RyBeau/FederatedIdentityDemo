using MediatR;

namespace AuthAPI.Handlers.Logout
{
    public class LogoutQuery : IRequest<bool>
    {
        public string SessionId { get; set; }
    }
}
