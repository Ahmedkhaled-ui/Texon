
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Texon.Service.Abstraction.IService;

namespace E_Commerce.Presentation.Attrabute
{

    //: Attribute, IAsyncActionFilter
    //Cash invalidation ==> want Search for a way to invalidate cash when data changes
    internal class CashRedisAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            // 1 get cash Service using DI
            var cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            // 2create Cash Key using quary string => orderBy
            string Key = GenerateKay(context.HttpContext.Request);

            // 3 Search  for cash value
            var cashKey = await cashService.GetCashAsync(Key);
            ///3.1 Exists => return Response && don not invoke the action


            if (cashKey is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cashKey,
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/json"
                };
                return;
            }
            ///3.2 Not Exists => invoke the action
            var Excuted = await next.Invoke();
            //4 check For Ok Object Result Then Cash
            if (Excuted.Result is OkObjectResult okObjectResult)
            {
                //5 Cash the Result
                await cashService.SetCashAsync(Key, okObjectResult.Value!, TimeSpan.FromMinutes(10));
            }

        }

        private static string GenerateKay(HttpRequest request)
        {
            var sb = new StringBuilder();
            foreach (var kvp in request.Query.OrderBy(opt => opt.Key))
            {
                //Key += $"{kvp.Key}-{kvp.Value}-";

                sb.Append($"{kvp.Key}-{kvp.Value}-");

            }
            return sb.ToString().Trim('-');
        }
    }
}
