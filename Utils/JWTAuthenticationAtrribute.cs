using System;
using System.Linq;
using DiscordRipoff.Data;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordRipoff.Utils {
    public class JWTAuthenticationAttribute : ActionFilterAttribute{
        public override void OnActionExecuting(ActionExecutingContext context) {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Split(" ").Last();
            var serviceProvider = context.HttpContext.RequestServices;
            var jwtService = serviceProvider.GetServices<JWTService>().FirstOrDefault();
            var dbContext = serviceProvider.GetServices<AppDbContext>().FirstOrDefault();
            if(dbContext == null || jwtService == null || !jwtService.ValidateTokent(dbContext, token)) {
                context.Result = new UnauthorizedResult();
            } 
            Console.WriteLine("unauthoriztion request");//TODO: testing
            base.OnActionExecuting(context);
        }
    }
}