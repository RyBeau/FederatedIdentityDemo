using Microsoft.AspNetCore.Http;

namespace FederatedIdentityDemo.Shared.Auth
{
    public static class CacheAuthHelper
    {
        public static string GenerateSessionId()
        {
            return Guid.NewGuid().ToString();
        }

        public static string? GetSessionId(HttpRequest request)
        {
            return request.Cookies[CacheAuthConstants.CookieName];
        }
    }
}
