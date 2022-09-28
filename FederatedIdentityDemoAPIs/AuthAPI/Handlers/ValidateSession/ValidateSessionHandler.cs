using AuthAPI.Responses;
using FederatedIdentityDemo.Shared.Services.Redis;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AuthAPI.Handlers.ValidateSession
{
    public class ValidateSessionHandler : IRequestHandler<ValidateSessionQuery, UserSummary?>
    {
        public readonly IDistributedCache _cache;
        public ValidateSessionHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public Task<UserSummary?> Handle(ValidateSessionQuery request, CancellationToken cancellationToken)
        {
            return _cache.GetRecordAsync<UserSummary>(request.SessionId);
        }
    }
}
