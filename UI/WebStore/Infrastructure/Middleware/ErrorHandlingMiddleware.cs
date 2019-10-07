using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebStore.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ErrorHandlingMiddleware> _Looger;

        public ErrorHandlingMiddleware(RequestDelegate Next, ILogger<ErrorHandlingMiddleware> Looger)
        {
            _Next = Next;
            _Looger = Looger;
        }

        public async Task Invoke(HttpContext Context)
        {
            try
            {
                // Предварительная обработка Context
                await _Next(Context);
                // Постобработка
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(Context, error);
                throw;
            }
        }

        private Task HandleExceptionAsync(HttpContext Context, Exception Error)
        {
            _Looger.LogError(Error, "Ошибка при обработке запроса {0}", Context.Request.Path);
            return Task.CompletedTask;
        }
    }
}
