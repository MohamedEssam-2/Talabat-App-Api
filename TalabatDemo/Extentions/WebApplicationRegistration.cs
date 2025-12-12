using Domain.Contracts;
using TalabatDemo.CustomMiddleWare;

namespace TalabatDemo.Extentions
{
    public static class WebApplicationRegistration
    {
        public async static void SeedDataAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var objectDataDeeding = scope.ServiceProvider.GetRequiredService<IDataSeed>();
            await objectDataDeeding.SeedDataAsync();
            await objectDataDeeding.IdentityDataSeeding(); //Seeding Identity Users and Roles
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }
    }
}
