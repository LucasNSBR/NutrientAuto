using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NutrientAuto.CrossCutting.IoC.Configuration.Context;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Identity.Data.Context;
using NutrientAuto.Identity.Domain.Aggregates.RoleAggregate;
using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;
using NutrientAuto.Identity.Domain.Context;
using NutrientAuto.Identity.Domain.Models.Services.UserAggregate;
using NutrientAuto.Identity.Domain.Services.RoleAggregate;
using NutrientAuto.Identity.Service.Services.Account;
using NutrientAuto.Identity.Service.Services.User;
using System;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Context
{
    public static partial class ContextDependencyInjectionExtensions
    {
        public static IServiceCollection AddIdentityContext(this IServiceCollection services, IConfiguration configuration, Action<IdentityContextOptions> setupAction)
        {
            IdentityContextOptions options = new IdentityContextOptions();
            setupAction(options);

            services.AddDbContext<ApplicationIdentityDbContext>(opt =>
                 opt.UseSqlServer(configuration.GetConnectionString("SqlServerMain")));

            services.AddIdentity<NutrientIdentityUser, NutrientIdentityRole>(cfg =>
            {
                cfg.User = options.UserOptions;
                cfg.Password = options.PasswordOptions;
                cfg.SignIn = options.SignInOptions;
            })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddUserManager<NutrientUserManager>()
            .AddSignInManager<NutrientSignInManager>()
            .AddRoleManager<NutrientRoleManager>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IApplicationIdentityDbContext, ApplicationIdentityDbContext>(provider => provider.GetRequiredService<ApplicationIdentityDbContext>());
            services.AddScoped<IUnitOfWork<IApplicationIdentityDbContext>, UnitOfWork<IApplicationIdentityDbContext>>();

            return services;
        }
    }
}
