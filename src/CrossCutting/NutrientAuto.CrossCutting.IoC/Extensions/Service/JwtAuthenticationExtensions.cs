using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NutrientAuto.CrossCutting.IoC.Configuration.Service;
using NutrientAuto.Identity.Domain.Services.Token.Factories;
using NutrientAuto.Identity.Domain.Services.Token.SigningServices;
using NutrientAuto.Identity.Service.Services.Token.Configuration;
using System;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Service
{
    public static partial class ServiceDependencyInjectionExtensions
    {
        public static AuthenticationBuilder AddJwtAuthentication(this AuthenticationBuilder authenticationBuilder, Action<JwtAuthenticationOptions> setupAction)
        {
            JwtAuthenticationOptions options = new JwtAuthenticationOptions();
            setupAction(options);

            ConfigureServiceOptions(authenticationBuilder, options);

            authenticationBuilder.Services.AddSingleton<ISigningService, SigningService>();
            authenticationBuilder.Services.AddSingleton<IJwtFactory, JwtFactory>();

            authenticationBuilder.AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = options.ValidateAudience,
                    ValidateIssuer = options.ValidateIssuer,
                    ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
                    ValidateLifetime = options.ValidateLifetime,
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    ClockSkew = options.ClockSkew,
                    IssuerSigningKey = authenticationBuilder.Services.BuildServiceProvider().GetService<ISigningService>().SecurityKey
                };

                cfg.SaveToken = true;
            });

            return new AuthenticationBuilder(authenticationBuilder.Services);
        }

        private static void ConfigureServiceOptions(AuthenticationBuilder authenticationBuilder, JwtAuthenticationOptions options)
        {
            authenticationBuilder.Services.Configure<JwtTokenOptions>(cfg =>
            {
                cfg.Issuer = options.Issuer;
                cfg.Audience = options.Audience;
                cfg.ExpiresInSeconds = options.ExpiresInSeconds;
                cfg.IssuedAt = DateTime.Now;
                cfg.NotBefore = DateTime.Now;
            });

            authenticationBuilder.Services.Configure<SigningOptions>(cfg =>
            {
                cfg.SALT_KEY = options.SALT_KEY;
            });
        }
    }
}
