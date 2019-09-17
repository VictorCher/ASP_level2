using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebStore.Infrastructure.Filters
{
    internal class ResultFilter : IResultFilter
    {
        #region Implementation of IResultFilter

        public void OnResultExecuting(ResultExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}