using Academic.Core.Helpers;
using Academic.Repository.Data;
using Academic.Repository.Repositories;
using Academic.Services.Abstractions;
using Academic.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesLayesrServices
            (this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<IInstructorsServices, InstructorServices>();
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<IPathServices, PathServices>();
            services.AddScoped<IModuleServices, ModuleServices>();
            services.AddScoped<IIdentityUserServices, IdentityUserServices>();
            services.AddScoped<ITokenServices, TokenServices>();

            var jwtConfig = new JwtHelper();
            configuration.GetSection("Jwt").Bind(jwtConfig);

            services.AddAuthServices(jwtConfig);


            return services;
        }
        private static IServiceCollection AddAuthServices(this IServiceCollection services, JwtHelper jwtConfig)
        {

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtConfig.JwtIssuer,
                        ValidAudience = jwtConfig.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JwtKey)),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            return services;
        }
    }
}
