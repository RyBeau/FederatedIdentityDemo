using FederatedIdentityDemo.Shared.Services.Redis;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AuthAPI.Handlers.Logout
{
    public class LogoutQueryHandler : IRequestHandler<LogoutQuery, bool>
    {

        private readonly IDistributedCache _cache;

        public LogoutQueryHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> Handle(LogoutQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await _cache.RemoveRecordAsync(request.SessionId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
