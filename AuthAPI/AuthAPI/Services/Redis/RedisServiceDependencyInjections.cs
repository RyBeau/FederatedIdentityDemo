namespace AuthAPI.Services.Redis
{
    public static class RedisServiceDependencyInjections
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, RedisConfiguration config)
        {
            return services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString();
                options.InstanceName = config.Prefix + "_";
            });
        }

    }
}
