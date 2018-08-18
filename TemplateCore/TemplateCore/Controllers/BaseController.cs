using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TemplateCore.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            var i = 0;
            i++;
            var j = i + 5;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            var i = 0;
            i++;
            var j = i + 5;
        }
    }
}