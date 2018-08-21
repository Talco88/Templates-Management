using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TemplateCore.Models;

namespace TemplateCore.Controllers
{
    public class TemplateController : ApiController
    {
        [HttpPost]
        public BaseWebResponce<string> Template([FromBody]dynamic requestBody)
        {
            return new BaseWebResponce<string>()
            {
                StatusCode = 0,
                Status = "OK",
                RetObject = "data"
            };
        }

        [HttpPost]
        public BaseWebResponce<string> SearchTemplate([FromBody]dynamic requestBody)
        {
            return new BaseWebResponce<string>()
            {
                StatusCode = 0,
                Status = "OK",
                RetObject = "data"
            };
        }


    }
}