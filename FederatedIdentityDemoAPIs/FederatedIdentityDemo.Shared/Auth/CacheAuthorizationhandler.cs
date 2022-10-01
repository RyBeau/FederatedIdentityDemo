using FederatedIdentityDemo.Models;
using FederatedIdentityDemo.Shared.Services.Redis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FederatedIdentityDemo.Shared.Auth
{
    public class CacheAuthenticationOptions : AuthenticationSchemeOptions
    {

    }

    public class CacheAuthorizationhandler : AuthenticationHandler<CacheAuthenticationOptions>
    {
        private readonly IDistributedCache _cache;

        public CacheAuthorizationhandler(
                    IOptionsMonitor<CacheAuthenticationOptions> options,
                    ILoggerFactory logger,
                    UrlEncoder encoder,
                    ISystemClock clock,
                    IDistributedCache cache)
            : base(options, logger, encoder, clock)
        {
            _cache = cache;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Cookies.Where(x => x.Key == "session_id").Any())
            {
                return AuthenticateResult.Fail("No session cookie found");
            }
            var cookie = Request.Cookies.Where(x => x.Key == "session_id").First();

            var user = await _cache.GetRecordAsync<UserSummary>(cookie.Value);

            if (user == null)
            {
                return AuthenticateResult.Fail("Session ID is invalid");
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Role, user.role),
                };
            var identity = new ClaimsIdentity(claims, "CacheSessionAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
