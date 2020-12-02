
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiscordRipoff
{
    public class Startup
    {
        public IConfiguration config { get; set; }
        public Startup(IConfiguration configuration) {
            this.config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddMvc();
            services.AddControllersWithViews();
            
            var conenctionString = config["ConnectionStrings:SqliteDb"];
            // Console.WriteLine(conenctionString);
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlite(conenctionString);
            });

            services.AddScoped<UserServices>();

            var secretKey = config["JWT:Secret"];
            services.AddSingleton<JWTService>(services => {
                return new JWTService(secretKey);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
