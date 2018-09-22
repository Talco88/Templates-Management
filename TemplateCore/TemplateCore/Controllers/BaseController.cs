using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TemplateCore.Models;

namespace TemplateCore.Controllers
{
    public class BaseController : ApiController
    {
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