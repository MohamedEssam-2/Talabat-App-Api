
using Domain.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens.Experimental;
using Presistence_Layer;
using Presistence_Layer.Data;
using Presistence_Layer.Repositories;
using Services_Abstraction;
using Services_Layer;
using Services_Layer.ServiceManger;
using Shared.Error_Models;
using TalabatDemo.CustomMiddleWare;
using TalabatDemo.Extentions;
using TalabatDemo.Factory;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace TalabatDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            builder.Services.AddSwaggerService();
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();


            builder.Services.AddInfrastructureServices(builder.Configuration);
            //builder.Services.AddDbContext<StoreDbContext>( options =>
            //{
            //            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});
            //builder.Services.AddScoped<IDataSeed, DataSeed>();  
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddApplicationService(builder.Configuration);
            //ApplicationServicesRegistration.AddApplicationService(builder.Services);
            //Services.AddAutoMapper(x => { }, typeof(ServiceLayerAssemblyReference).Assembly);//Add All profiles in Service Layer Assembly
            //Services.AddScoped<IServiceManager, ServiceManager>();


            builder.Services.AddWebApplicationServices(builder.Configuration);
            //builder.Services.Configure<ApiBehaviorOptions>((options) =>
            //{
            //    options.InvalidModelStateResponseFactory = ApiResponseFactory.GenrateApiValidationErrorResponse;

            //});

            var app = builder.Build();

              app.SeedDataAsync();
            //var scope = app.Services.CreateScope();
            //var objectDataDeeding = scope.ServiceProvider.GetRequiredService<IDataSeed>();
            //objectDataDeeding.SeedDataAsync();

            app.UseCustomExceptionMiddleWare();
            //app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            //app.Use(async (RequestContext, NextMiddleWarew) =>
            //{
            //    Console.WriteLine("Something Went Wrong");
            //    await NextMiddleWarew.Invoke();
            //    Console.WriteLine("Wating Response");
            //});


            app.MapControllers();

            app.Run();


           
        }
    }
}
