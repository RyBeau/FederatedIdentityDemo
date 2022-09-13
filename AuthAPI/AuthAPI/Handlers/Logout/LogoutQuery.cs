using MediatR;

namespace AuthAPI.Handlers.Logout
{
    public class LogoutQuery : IRequest<string>
    {
        public string SessionId { get; set; }
    }
}
