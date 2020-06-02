using API.Contracts;
using API.Models;
using System.Linq;
using System.Web.Mvc;

namespace API.Filters
{
    public class AuthorizeTokenValidityAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string token = filterContext.HttpContext.Request.Headers.GetValues("AuthToken").First();

            JWTContainerModel containerModel = new JWTContainerModel();
            JWTService jWTService = new JWTService(containerModel.SecretKey);

            if (jWTService.IsTokenValid(token))
            {
                filterContext.Result = new HttpStatusCodeResult(200, "");
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(401, "");
            }
        }
    }
}