using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services;

namespace Talabat.APIs.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _liveTime;

        public CachedAttribute(int liveTime)
        {
            _liveTime = liveTime;
        }   
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

           var cachService = context.HttpContext.RequestServices.GetRequiredService<IResponseCachService>();

            var cachKey = GenerateCachKeyFromRequest(context.HttpContext.Request);

            var cachedResponse = await cachService.GetCachedResponseAsync(cachKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };

                context.Result = contentResult;
                return;
            }


           var executedEndpointContext = await next();

            // endPoint Result Sucess
            if (executedEndpointContext.Result is OkObjectResult okObjectResult) 
            {
                // Cach the Result of Endpoint
                await cachService.CachResponseAsync(cachKey,okObjectResult.Value,TimeSpan.FromSeconds(_liveTime)); 
            }
        }

        private string GenerateCachKeyFromRequest(HttpRequest request)
        {
            var cachedkey = new StringBuilder();

            cachedkey.Append(request.Path);

            foreach (var (key,value) in request.Query.OrderBy(x => x.Key))
            {
                cachedkey.Append($"|{key}-{value}");
            }

            return cachedkey.ToString();
        }
    }
}
