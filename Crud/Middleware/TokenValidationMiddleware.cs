using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Crud.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IConfiguration _config;
        public TokenValidationMiddleware(RequestDelegate requestDelegate, IConfiguration config)
        {
            _requestDelegate = requestDelegate;
            _config = config;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = _config["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _config["Jwt:Audience"],
                    }, out SecurityToken validatedToken);

                    context.User = claims;
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid token.");
                    return;
                }
            }
            await _requestDelegate(context);
        }
    }
}
