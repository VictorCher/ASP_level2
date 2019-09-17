using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebStore.Infrastructure.Filters
{
    internal class AuthorizeFilter : IAuthorizationFilter
    {
        #region Implementation of IAuthorizationFilter

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}