using System.IdentityModel.Tokens.Jwt;
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
            context.Request.Path == "/Account/Registration")
        {
            await _next(context);
            return;
        }

        if (!context.Request.Cookies.ContainsKey("token") &&
            !context.Request.Headers.Authorization.Any(x => x.Contains("Bearer ")))
        {
            context.Response.Redirect("/Account/Authentication");
            return;
        }


        try
        {
            string? token;
            if (context.Request.Cookies.ContainsKey("token"))
            {
                token = context.Request.Cookies["token"];
            }
            else
            {
                var bearer = context.Request.Headers.Authorization.FirstOrDefault(x => x.Contains("Bearer "));
                token = bearer.Replace("Bearer ","");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            context.User = claims;
        }
        catch (Exception ex)
        {
            context.Response.Redirect("/Account/Authentication");
            return;
        }

        await _next(context);
    }
}