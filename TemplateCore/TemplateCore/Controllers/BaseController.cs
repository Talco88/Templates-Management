using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using TemplateCore.Models;

namespace TemplateCore.Controllers
{
    public class BaseController : ApiController
    {
        private readonly string SESSION_USER_DATA = "UserData";
        /// <summary>
        /// CTOR
        /// </summary>
        public BaseController()
        {
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        protected void RemovePrincipal()
        {
            HttpContext.Current.User = null;
        }

        protected bool isUserLogedOn()
        {
            return (HttpContext.Current.Session[SESSION_USER_DATA] != null);
        }

        protected string userEmail()
        {
            var userData = HttpContext.Current.Session[SESSION_USER_DATA] as PrincipalUser;
            if (userData != null)
            {
                try
                {
                    return userData.Identity.Name;
                }
                catch
                {
                    // do nothing
                }
            }
            throw new Exception($"Unable to get user log in information");
        }

        protected void SetPrincipal(string iUserMail, bool iIsAdmin)
        {
            var principal = createUserIdentety(iUserMail, iIsAdmin);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[SESSION_USER_DATA] = principal;
                //HttpContext.Current.User = principal;
            }
        }

        protected IPrincipal createUserIdentety(string iUserMail, bool iIsAdmin)
        {
            PrincipalUser user = new PrincipalUser(new IdentityUser(iUserMail, iIsAdmin));
            return user;
        }

        protected BaseWebResponce<object> SetExceptionResponce(Exception exception)
        {
            return new BaseWebResponce<object>()
            {
                StatusCode = 1,
                Status = "Error",
                RetObject = exception.Message
            };
        }

        protected BaseWebResponce<T> SetSuccessResponce<T>(T retrunObject)
        {
            return new BaseWebResponce<T>()
            {
                StatusCode = 0,
                Status = "OK",
                RetObject = retrunObject
            };
        }
    }
}