using API.Contracts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.Filters
{
    public class TokenValidityActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string token = filterContext.ControllerContext.Request.Headers.GetValues("AuthToken").First();

            JWTContainerModel containerModel = new JWTContainerModel();
            JWTService jWTService = new JWTService(containerModel.SecretKey);

            if (jWTService.IsTokenValid(token))
            {
                //filterContext.Response.StatusCode = System.Net.HttpStatusCode.Accepted; // Result = new HttpStatusCodeResult(200, "");
                //filterContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.Accepted);
            }
            else
            {
                //filterContext.Result = new HttpStatusCodeResult(401, "");
                filterContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}