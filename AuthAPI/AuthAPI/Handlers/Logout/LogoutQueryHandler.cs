using MediatR;

namespace AuthAPI.Handlers.Logout
{
    public class LogoutQueryHandler : IRequestHandler<LogoutQuery, string>
    {
        public Task<string> Handle(LogoutQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Logged Out");
        }
    }
}
