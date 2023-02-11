using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace FederatedIdentityDemo.Shared.Auth
{
    public static class CacheAuthHelper
    {
        public static string GenerateSessionId()
        {
            var keyBytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(keyBytes);
        }

        public static string? GetSessionId(HttpRequest request)
        {
            return request.Cookies[CacheAuthConstants.CookieName];
        }
    }
}
