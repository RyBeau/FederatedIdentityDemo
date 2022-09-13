using MediatR;

namespace AuthAPI.Handlers.ValidateSession
{
    public class ValidateSessionQuery : IRequest<string>
    {
        public string SessionId { get; set; }
    }
}
