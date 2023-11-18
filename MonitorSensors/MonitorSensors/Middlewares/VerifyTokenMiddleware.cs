using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace MonitorSensors.Middlewares;

public class VerifyTokenMiddleware
{
    private readonly RequestDelegate _next;

    public VerifyTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, TokenValidationParameters tokenValidationParameters)
    {
        if (context.Request.Path == "/Account/Authentication" || 
            context.Request.Path == "/Account/Registration" )
        {
            await this._next(context);
            return;
        }
        
        if (!context.Request.Cookies.ContainsKey("token"))
        {
            context.Response.StatusCode = 401;
            context.Response.Redirect("/Account/Authentication");
            return;
        }

        try
        {
            var token = context.Request.Cookies["token"];
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            context.User = claims;

        }
        catch(Exception ex)
        {
            context.Response.StatusCode = 401;
            context.Response.Redirect("/Account/Authentication");
            return;
        }
     
        await this._next(context);
    }

   

}