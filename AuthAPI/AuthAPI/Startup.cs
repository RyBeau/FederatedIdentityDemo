using AuthAPI.Services.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AuthAPI
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup (IConfiguration configuration)
        {
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddScoped<Repository>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
                .UseRouting()
                .UseSwagger()
                .UseSwaggerUI();
            app.UseEndpoints(x => x.MapControllers());

        }
    }
}
