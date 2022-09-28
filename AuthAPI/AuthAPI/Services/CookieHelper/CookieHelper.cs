using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Services.CookieHelper
{
    public static class CookieHelper
    {
        public static IActionResult AddCookie(this IActionResult result, HttpResponse response,string cookieKey, string sessionId)
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
