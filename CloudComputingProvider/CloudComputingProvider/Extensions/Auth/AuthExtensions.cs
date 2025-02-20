using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CloudComputingProvider.Extensions.Auth
{
    public static class AuthExtensions
    {
        public static void RegisterAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var securityKey = configuration["Auth:Secret"];

            if (string.IsNullOrWhiteSpace(securityKey))
                throw new ArgumentNullException("Security key not available for auth initialization");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
                    };
                });
        }
    }
}
