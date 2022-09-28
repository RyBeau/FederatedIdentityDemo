using AuthAPI.Responses;
using MediatR;

namespace AuthAPI.Handlers.ValidateSession
{
    public class ValidateSessionQuery : IRequest<UserSummary>
    {
        public string SessionId { get; set; }
    }
}
