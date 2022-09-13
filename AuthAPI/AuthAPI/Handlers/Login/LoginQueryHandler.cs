using MediatR;

namespace AuthAPI.Handlers.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        public Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult($"Login successful for {request.Username}");
        }
    }
}
