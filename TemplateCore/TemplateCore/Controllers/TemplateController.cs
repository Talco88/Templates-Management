using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TemplateCore.Models;
using TemplateCoreBusiness.Engine;

namespace TemplateCore.Controllers
{
    public class TemplateController : ApiController
    {
        protected IAppEngine appEngine { get; }

        public TemplateController()
        {
            appEngine = AppEngineBuilder.GetAppEngine();
        }

        [HttpPost]
        public BaseWebResponce<string> GetTemplate([FromBody]dynamic requestBody)
        {
            return appEngine.GetTemplate(requestBody.Data.templateName.Value);
        }

        [HttpPost]
        public BaseWebResponce<string> SearchTemplate([FromBody]dynamic requestBody)
        {
            return appEngine.GetTemplateFromSearch(requestBody.Data.searchKey.Value);
        }


    }
}