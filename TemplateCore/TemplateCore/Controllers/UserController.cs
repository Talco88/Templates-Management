using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TemplateCore.Models;

namespace TemplateCore.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        public BaseWebResponce<string> Login([FromBody]dynamic requestBody)
        {
            return new BaseWebResponce<string>()
            {
                StatusCode = 0,
                Status = "OK",
                RetObject = requestBody.Data.username.Value + " Is logged on"
            };
        }

        [HttpPost]
        public BaseWebResponce<string> RegisterNewUser([FromBody]dynamic requestBody)
        {
            return new BaseWebResponce<string>()
            {
                StatusCode = 0,
                Status = "OK",
                RetObject = requestBody.Data.username.Value + " Is Registered"
            };
        }

        [HttpPost]
        public BaseWebResponce<string> IsLogin([FromBody] dynamic requestBody)
        {
            return new BaseWebResponce<string>()
            {
                StatusCode = 0,
                Status = "OK",
                RetObject = "user log in"
            };
        }

        [HttpPost]
        public BaseWebResponce<string> Logout([FromBody] dynamic requestBody)
        {
            return new BaseWebResponce<string>()
            {
                StatusCode = 0,
                Status = "OK",
                RetObject = "user logged off"
            };
        }

    }
}