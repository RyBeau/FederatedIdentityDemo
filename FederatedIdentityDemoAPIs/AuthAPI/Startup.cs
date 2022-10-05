using AuthAPI.Services.DB;
using FederatedIdentityDemo.Shared.Auth;
using FederatedIdentityDemo.Shared.Services.Redis;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI
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

            var dbConfig = _config.GetSection("database").Get<DbConfiguration>();
            services.AddDbContext<Context>(options =>
                options.UseMySql(dbConfig.GetConnectionString(),
                ServerVersion.AutoDetect(dbConfig.GetConnectionString())
                )
            );

            var redisConfig = _config.GetSection("Redis").Get<RedisConfiguration>();

            services.AddRedis(redisConfig);

            services.AddScoped<Repository>();
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
