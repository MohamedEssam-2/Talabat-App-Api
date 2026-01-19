using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services_Abstraction;

namespace Presentation_Layer.Attributes
{
    internal class CacheRedisAttribute(int duration =120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Get Cache Service
            var CacheService=context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
            //Generate Key 
            string key = GenerateKey(context.HttpContext.Request);
            //Check Cache
            var result = await CacheService.GetCacheAsync(key);
            if (result != null)
            {
                context.Result = new ContentResult()
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            //check the result 
            var resultContext = await next.Invoke();
            if (resultContext.Result is OkObjectResult okObject)
            {
                await CacheService.SetCacheAsync(key, okObject, TimeSpan.FromSeconds(duration));
            }

        }

        private string GenerateKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
