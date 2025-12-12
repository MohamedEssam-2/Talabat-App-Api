using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence_Layer.Data;
using Presistence_Layer.Identity;
using Presistence_Layer.Repositories;
using StackExchange.Redis;

namespace Presistence_Layer
{
    public static class InfrastructureApplicationRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services,IConfiguration _configuration)
        {
            Services.AddScoped<IDataSeed, DataSeed>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(_configuration.GetConnectionString("RedisConnectionString"));
            });

            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"));
            });

            //AddIdentity => Not Working Here ,Missing some services in Infrastructure Layer
            //Services.AddIdentity<ApplicationUser,IdentityRole>()
            //    .AddEntityFrameworkStores<StoreIdentityDbContext>()
            //So We Use AddIdentityCore
            Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return Services;
        }
    }
}
