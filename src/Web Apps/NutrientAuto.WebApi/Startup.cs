using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NutrientAuto.CrossCutting.IoC.Extensions.Context;
using NutrientAuto.CrossCutting.IoC.Extensions.Service;
using NutrientAuto.WebApi.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NutrientAuto.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCoreContext();
            services.AddCommunityContext(Configuration);

            services.AddIdentityContext(Configuration, opt =>
            {
                opt.UserOptions = new UserOptions
                {
                    AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@",
                    RequireUniqueEmail = true
                };

                opt.PasswordOptions = new PasswordOptions
                {
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequireDigit = false,
                    RequiredUniqueChars = 0,
                    RequiredLength = 8
                };

                opt.SignInOptions = new SignInOptions
                {
                    RequireConfirmedEmail = false,
                    RequireConfirmedPhoneNumber = false
                };
            });

            services.AddHttpServices();

            services.AddServiceBus(opt =>
            {
                opt.HostAddress = Configuration["MassTransit:RabbitMqHost"];
                opt.RabbitMqHostUser = Configuration["MassTransit:RabbitMqHostUser"];
                opt.RabbitMqHostPassword = Configuration["MassTransit:RabbitMqHostPassword"];
                opt.RabbitMqQueueName = Configuration["MassTransit:RabbitMqQueueName"];
            });

            services.AddAutomapper();

            services.AddStorageService(opt =>
            {
                opt.AccountName = Configuration["Storage:AccountName"];
                opt.AccountKey = Configuration["Storage:AccountKey"];
                opt.ContainerName = Configuration["Storage:ContainerName"];
            });

            services.AddEmailService(opt =>
            {
                opt.SenderAddress = Configuration["SendGrid:SenderAddress"];
                opt.SenderName = Configuration["SendGrid:SenderName"];
                opt.SendGridKey = Configuration["SendGrid:SendGridKey"];
            });

            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new Info
                {
                    Title = "Nutrient v1",
                    Contact = new Contact
                    {
                        Name = "Lucas Campos",
                        Url = "http://github.com/lucasnsbr"
                    },
                    Version = "v1",
                });
            });

            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtAuthentication(cfg =>
            {
                cfg.ValidateAudience = true;
                cfg.ValidateIssuer = true;
                cfg.ValidateIssuerSigningKey = true;
                cfg.ValidateLifetime = true;
                cfg.Issuer = Configuration["JwtToken:Issuer"];
                cfg.Audience = Configuration["JwtToken:Audience"];
                cfg.ExpiresInSeconds = Configuration.GetValue<int>("JwtToken:ExpiresInSeconds");
                cfg.IssuedAt = DateTime.Now;
                cfg.NotBefore = DateTime.Now;
                cfg.ClockSkew = TimeSpan.Zero;
                cfg.SALT_KEY = Configuration["SALT_KEY"];
            });

            services.AddAuthorization(cfg => cfg.AddPolicy("AdminAccount",
                cfgPolicy => cfgPolicy.RequireClaim("AdminAccount", "true")));

            services.AddAuthorization(cfg => cfg.AddPolicy("SupportAccount",
                cfgPolicy => cfgPolicy.RequireClaim("SupportAccount", "true")));

            services.AddAuthorization(cfg => cfg.AddPolicy("ActiveNutritionist",
                cfgPolicy => cfgPolicy.RequireClaim("ActiveNutritionist", "true")));

            services.AddAuthorization(cfg => cfg.AddPolicy("ActiveProfile",
                cfgPolicy => cfgPolicy.RequireClaim("ActiveProfile", "true")));

            services.AddHealthChecks()
                .AddSqlServer(Configuration["ConnectionStrings:SqlServerMain"])
                .AddAzureBlobStorage(Configuration["Storage:AccountKey"])
                .AddRabbitMQ(Configuration["MassTransit:RabbitMqHost"]);

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddResponseCompression();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(cfg =>
                {
                    cfg.AllowAnyMethod();
                    cfg.AllowAnyHeader();
                    cfg.AllowAnyOrigin();
                    cfg.AllowCredentials();
                });
            }
            else
            {
                app.UseHealthChecks(Configuration["HealthCheckEndpoint"], new HealthCheckOptions
                {
                    ResponseWriter = (context, report) =>
                    {
                        object result = new
                        {
                            status = report.Status.ToString(),
                            services = report.Entries.Select(kv =>
                            {
                                return new KeyValuePair<string, object>(kv.Key, new
                                {
                                    name = kv.Key,
                                    description = kv.Value.Description,
                                    status = kv.Value.Status,
                                    data = kv.Value.Data
                                });
                            })
                        };

                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                    }

                });

                app.UseGlobalExceptionHandler();
            }

            CultureInfo[] supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseResponseCompression();
            app.UseAuthentication();

            app.UseNotFoundLogger();

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint(Configuration["SwaggerEndpoint"], "Nutrient v1");
            });

            app.UseMvc();
        }
    }
}
