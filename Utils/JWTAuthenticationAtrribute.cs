using System;
using System.Linq;
using DiscordRipoff.Data;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordRipoff.Utils {
    public class JWTAuthenticationAttribute : ActionFilterAttribute{ //TODO: : AuthAttribute
        public override void OnActionExecuting(ActionExecutingContext context) {
            var token = context.HttpContext
                            .Request
                            .Headers["Authorization"]
                            .ToString()
                            .Split(" ")
                            .Last();
            var serviceProvider = context.HttpContext.RequestServices;
            var jwtService = serviceProvider.GetServices<JWTService>().FirstOrDefault();
            var dbContext = serviceProvider.GetServices<AppDbContext>().FirstOrDefault();
            if(token == null 
            || token == "" 
            || dbContext == null 
            || jwtService == null 
            || !jwtService.ValidateTokent(dbContext, token)) 
            {
                Console.WriteLine("unauthoriztion request:");
                context.Result = new UnauthorizedResult();
            } 

            //TODO: test
            Console.WriteLine(context.HttpContext.Request.Path);
            // Console.WriteLine(context.HttpContext.Request.Query);
            foreach(var q in context.HttpContext.Request.Query) {
                Console.WriteLine($"{q.Key}: {q.Value}");
            }
            foreach( var s in context.HttpContext.Request.Headers) {
                Console.WriteLine($"{s.Key}: {s.Value}");
            }
            base.OnActionExecuting(context);
        }
    }
}