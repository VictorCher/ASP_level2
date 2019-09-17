using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebStore.Infrastructure.Filters
{
    internal class ExceptionFilter : IExceptionFilter
    {
        #region Implementation of IExceptionFilter

        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}