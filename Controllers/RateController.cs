using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/v1")]
    public class RateController : ApiController
    {
        [HttpGet]
        [Route("medals")]        
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("medals/{id}")]
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        
    }
}