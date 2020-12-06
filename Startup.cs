
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Services;
using DiscordRipoff.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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
            services.AddSpaStaticFiles(options => {
                options.RootPath = "client-app/dist";
            });
            



            //RM: basic mvc middlerware

            // services.AddMvc(); //= AddControllersWithViews + AddRazorPages //RM
            
            // services.AddControllersWithViews(); //RM=
            // services.AddMvcCore()
            //         .AddApiExplorer()
            //         .AddAuthorization()
                    // .AddCors()
                    // .AddDataAnnotations()
                    // .AddFormatterMappings()
                    // .AddCacheTagHelper()
                    // .AddViews()
                    // .AddRazorViewEngine();

            // services.AddRazorPages(); //RM=
            // services.AddMvcCore()
            //         .AddAuthorization() 
            //         .AddDataAnnotations() 
            //         .AddCacheTagHelper() 
            //         .AddRazorPages();

            // services.AddControllers(); //RM=
            // services.AddMvcCore()
            //         .AddApiExplorer()
            //         .AddAuthorization()
            //         .AddCors()
            //         .AddDataAnnotations()
            //         .AddFormatterMappings();


            //RM: detail
            // services.AddMvcCore() // add basic thing like: model binding, routing, filter, controller, constraint ...
            //         .AddApiExplorer() // alow get metadata of api to debug or generate document ...
            //         .AddAuthorization() // call services.AddAuthorization() and add config
            //         .AddCors() // call servicse.AddCors() and add config
            //         .AddDataAnnotations() // call services.AddDataAnnotations() // [dataAnotation] use on model // CANT call from services
            //         .AddFormatterMappings() // call services.AddFormatterMappings() ... CANT call from services
            //         // A filter that will use the format value in the route data or query 
            //         // string to set the content type on an ObjectResult returned from an action.
            //         .AddCacheTagHelper() // call services.AddCacheTagHelper() ... CANT call from services// dung caches tag
            //         .AddViews() // call AddDataAnnotations() and services.AddViewServices . CANT call from services
            //         // add a lot of services relate to view
            //         .AddRazorViewEngine() // addView(), services.AddRazorViewEngine(). CANT call from services
            //         // add view of razor
            //         .AddRazorPages(); // AddRazorViewEngine, services.AddRazorPages. 

            // services.AddCors(options => {
            //     // add Policy on how to allow
            // });
            // services.AddRazorPages(); // add AddRazorViewEngine and other services
            
            services.AddControllers();
            services.AddCors();
            
            services.AddAuthentication(options => {
                options.AddScheme("name", config => {
                    
                });
            });
            
            var conenctionString = config.GetConnectionString("DbNow");
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlite(conenctionString);
                // options.UseMySQL(conenctionString);
            });

            services.AddScoped<UserServices>();

            var secretKey = config["JWT:Secret"];
            services.AddScoped<JWTService>(services => {
                return new JWTService(secretKey);
            });

            services.AddScoped<JWTAuthenticationAttribute>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseCors(config => {
                config.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });

            app.UseSpaStaticFiles();
            app.UseSpa(builder => {
                // builder.Options.DefaultPage = "/testDefaultPage";
                builder.Options.SourcePath = "client-app";
                if(env.IsDevelopment()) {
                    // builder.Options.PackageManagerCommand = "npm run serve";
                    // VueUtil.RunServe();
                    builder.UseProxyToSpaDevelopmentServer("http://localhost:8080/");
                }
            });
        }
    }
}
