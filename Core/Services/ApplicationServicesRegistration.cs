using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services_Abstraction;
using Services_Layer.Service;
using Services_Layer.Service.BasketService;
using Services_Layer.Service.OrderService;
using Services_Layer.Service.PaymentService;
using Services_Layer.Service.ProductService;
using Services_Layer.ServiceManger;
using Shared.Authenticaion;
namespace Services_Layer
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services,IConfiguration _configuration)
        {
            Services.AddAutoMapper(x => { }, typeof(ServiceLayerAssemblyReference).Assembly);//Add All profiles in Service Layer Assembly
            //Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            Services.Configure<JwtOptions>(_configuration.GetSection("JwtOptions"));


            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(serviceProvider => () => serviceProvider.GetRequiredService<IProductService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(serviceProvider => () => serviceProvider.GetRequiredService<IBasketService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(serviceProvider => () => serviceProvider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<Func<IOrderService>>(serviceProvider => () => serviceProvider.GetRequiredService<IOrderService>());

            Services.AddScoped<ICacheService,CacheService>();
            Services.AddScoped<Func<ICacheService>>(serviceProvider => () => serviceProvider.GetRequiredService<ICacheService>());

            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<Func<IPaymentService>>(serviceProvider => () => serviceProvider.GetRequiredService<IPaymentService>());
            return Services;
        }
    }
}
