using AuthAPI.Responses;
using AuthAPI.Services.DB;
using AuthAPI.Services.Redis;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AuthAPI.Handlers.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserSummary?>
    {
        private readonly Repository _repository;
        private readonly IDistributedCache _cache;

        public LoginQueryHandler(Repository repository, IDistributedCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<UserSummary?> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetUser(request.Username, request.Password);
            if (response != null)
            {
                await _cache.SetRecordAsync(request.SessionId, response);
            }
            return response;
        }
    }
}
