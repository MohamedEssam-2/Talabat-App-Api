using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services_Abstraction;
using Services_Layer.ServiceManger;
using Shared.Authenticaion;
namespace Services_Layer
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services,IConfiguration _configuration)
        {
            Services.AddAutoMapper(x => { }, typeof(ServiceLayerAssemblyReference).Assembly);//Add All profiles in Service Layer Assembly
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.Configure<JwtOptions>(_configuration.GetSection("JwtOptions"));
            return Services;
        }
    }
}
