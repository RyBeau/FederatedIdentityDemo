using Microsoft.Extensions.DependencyInjection;

namespace FederatedIdentityDemo.Shared.Auth
{
    public static class CacheAuthenticationDependencyInjections
    {
        public static void AddCacheAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CacheAuthConstants.SchemeName;
            }).AddScheme<CacheAuthenticationOptions, CacheAuthorizationhandler>(CacheAuthConstants.SchemeName, option => { });
        }
    }
}
