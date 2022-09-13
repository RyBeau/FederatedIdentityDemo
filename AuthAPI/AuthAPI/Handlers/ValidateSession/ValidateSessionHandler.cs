using MediatR;

namespace AuthAPI.Handlers.ValidateSession
{
    public class ValidateSessionHandler : IRequestHandler<ValidateSessionQuery, string>
    {
        public Task<string> Handle(ValidateSessionQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Validated");
        }
    }
}
