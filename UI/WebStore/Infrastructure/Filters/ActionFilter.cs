using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebStore.Infrastructure.Filters
{
    internal class ActionFilter : IActionFilter
    {
        #region Implementation of IActionFilter

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    internal class ActionFilterAsync : Attribute, IAsyncActionFilter
    {
        #region Implementation of IAsyncActionFilter

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // обработка context перед началом дальнейших действий

            await context.HttpContext.Response.WriteAsync("Действие отменено");

            //var next_task = next();

            // Набор действий, выполняемых параллельно действию контроллера

            //await next_task;

            // Обработка результата

            //throw new OperationCanceledException();

        }

        #endregion
    }
}