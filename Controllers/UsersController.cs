using API.Contracts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Net;
using API.Filters;

namespace API.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class UsersController : ApiController
    {
        //Use a tilde(~) on the method attribute to override the route prefix:
        //// GET /api/authors/1/books
        //[Route("~/api/authors/{authorId:int}/books")]
        //public IEnumerable<Book> GetByAuthor(int authorId) { ... }


        [HttpGet]
        [Route("")]
        // GET: Users
        public IEnumerable<string> Index()
        {
            return new string[] { "value1", "value2", "value1", "value2" };
        }


        [HttpPost]
        [Route("signin")]
        // GET api/<controller>
        public IHttpActionResult SignIn(UserModel userModel)
        {
            // go to db and verify user
            if (userModel.username == "A" && userModel.password == "123")
            {
                JWTContainerModel containerModel = new JWTContainerModel() { ExpireMinutes = 1 };
                containerModel.Claims = new Claim[] {
                    new Claim(ClaimTypes.Name, userModel.username),
                    new Claim("color", "red")
                };
                JWTService jWTService = new JWTService(containerModel.SecretKey);
                string token = jWTService.GenerateToken(containerModel);
                return Ok(token);
            }
            return NotFound();
        }

        [TokenValidityActionFilter]
        [HttpGet]
        [Route("medals")]
        public HttpResponseMessage Medals()
        {
            var headers = HttpContext.Current.Request.Headers;
            string token = headers.GetValues("AuthToken").First();

            JWTContainerModel containerModel = new JWTContainerModel();
            JWTService jWTService = new JWTService(containerModel.SecretKey);

            if (jWTService.IsTokenValid(token))
            {
                List<Medal> list = new List<Medal> {
                new Medal { id = "1", name = "GOLD", price="654654564"},
                new Medal { id = "2", name = "SILVER", price="77400"}
                };
                return Request.CreateResponse<List<Medal>>(System.Net.HttpStatusCode.OK, list);
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "لطفا مجددا لاگین کنید");
            }
        }


        [TokenValidityActionFilter]
        [HttpGet]
        [Route("medals/super")]
        public HttpResponseMessage MedalsSuper()
        {
            var headers = HttpContext.Current.Request.Headers;
            string token = headers.GetValues("AuthToken").First();

            JWTContainerModel containerModel = new JWTContainerModel();
            JWTService jWTService = new JWTService(containerModel.SecretKey);

            if (jWTService.IsTokenValid(token))
            {
                List<Claim> claimList = jWTService.GetTokenClaims(token).ToList();

                var color = jWTService.GetTokenClaims(token).Where(x => x.Type == "color").FirstOrDefault();
                
                return Request.CreateResponse<string>(HttpStatusCode.OK, color.Value);
                //return Request.CreateResponse<List<Claim>>(System.Net.HttpStatusCode.OK, claimList);
            }
            else
            {               
               return Request.CreateResponse(HttpStatusCode.Unauthorized, "لطفا مجددا لاگین کنید");
            }
        }





    }
}