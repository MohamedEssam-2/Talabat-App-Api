using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Microsoft.OpenApi.Models;
using Shared.Authenticaion;
using TalabatDemo.Factory;
namespace TalabatDemo.Extentions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(options =>
            {
            options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme()
            {
                In=ParameterLocation.Header,
                Name="Authorization",
                Type=SecuritySchemeType.ApiKey,
                Scheme="Bearer",
                Description="Enter 'Bearer' Followed By Spac And Your Token "
            });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference=new OpenApiReference()
                            {
                                Id="Bearer",
                                Type=ReferenceType.SecurityScheme
                            }
                          
                        },  new string[]{}
                    }

                });

            });
            return Services;
        }
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenrateApiValidationErrorResponse;

            });
            Services.ConfigureJwt(configuration);
            return Services;
        }
        public static void ConfigureJwt(this IServiceCollection Services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(config =>

            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey))


                };

            });
        }
    } 
}
