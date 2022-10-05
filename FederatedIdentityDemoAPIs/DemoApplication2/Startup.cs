using FederatedIdentityDemo.Shared.Auth;
using FederatedIdentityDemo.Shared.Services.Redis;
using MediatR;

namespace DemoApplication2
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCacheAuthentication();

            services.AddControllers();
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddMediatR(typeof(Startup));

            var redisConfig = _config.GetSection("Redis").Get<RedisConfiguration>();

            services.AddRedis(redisConfig);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseSwagger()
                    .UseSwaggerUI();
            }

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(x => x.MapControllers());

        }
    }
}
