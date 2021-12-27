using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SessionInterface;

namespace WebApi.Filters
{
    [ExcludeFromCodeCoverage]
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly ISessionLogic sessions;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                Guid token = Guid.Parse(context.HttpContext.Request.Headers["Authorization"]);
                var sessions = this.GetSessionLogic(context);
                if(!sessions.IsCorrectToken(token))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 403,
                        Content = "Not authorized"
                    };
                }
            }
            catch (ArgumentNullException)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "A token is required"
                };
            }
            catch (Exception)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Not valid token "
                };
            }
        }
        private ISessionLogic GetSessionLogic(AuthorizationFilterContext context)
        {
            var sessionType = typeof(ISessionLogic);

            return context.HttpContext.RequestServices.GetService(sessionType) as ISessionLogic;
        }
    }
}