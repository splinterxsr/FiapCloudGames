using FiapCloudGames.Infra.CrossCutting.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FiapCloudGames.Api.Configurations
{
    public static class JwtSecurityExtensions
    {
        public static IServiceCollection AddJwtSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>() ?? throw new Exception("JwtOptioñs não encontrado."); ;

            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;              

                var bytes = Encoding.UTF8.GetBytes(jwtOptions.Key);
                var symmetricSecurityKey = new SymmetricSecurityKey(bytes);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = symmetricSecurityKey,
                    ValidIssuers = jwtOptions.Issuers,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetService<ILogger<Program>>() ?? throw new Exception("Serviço de logger não encontrado.");

                        logger.LogInformation("Token de acesso válido. Acesso liberado.");

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetService<ILogger<Program>>() ?? throw new Exception("Serviço de logger não encontrado."); ;

                        logger.LogInformation("Token de acesso inválido. Acesso bloqueado.");

                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetService<ILogger<Program>>() ?? throw new Exception("Serviço de logger não encontrado.");

                        logger.LogInformation("Token de acesso inválido. Desafio enviado para cliente.");

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}