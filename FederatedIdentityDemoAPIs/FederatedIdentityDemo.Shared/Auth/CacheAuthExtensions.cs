﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FederatedIdentityDemo.Shared.Auth
{
    public static class CacheAuthExtensions
    {
        public static IActionResult AddCookie(this IActionResult result, HttpResponse response, string cookieKey, string sessionId)
        {
            response.Cookies.Append(
                cookieKey,
                sessionId,
                new CookieOptions()
                {
                    Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                }
                );
            return result;
        }

        public static IActionResult RemoveCookie(this IActionResult result, HttpResponse response, string cookieKey)
        {
            response.Cookies.Delete(cookieKey);
            return result;
        }
    }
}
