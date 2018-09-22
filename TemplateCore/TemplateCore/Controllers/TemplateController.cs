using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TemplateCore.Models;
using TemplateCoreBusiness.Engine;

namespace TemplateCore.Controllers
{
    public class TemplateController : BaseController
    {
        protected IAppEngine appEngine { get; }

        public TemplateController()
        {
            appEngine = AppEngineBuilder.GetAppEngine();
        }

        [HttpPost]
        public dynamic GetTemplate([FromBody]dynamic requestBody)
        {
            try
            {
                var template = appEngine.GetTemplate(requestBody.Data.templateName.Value);
                return SetSuccessResponce(template);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic SearchTemplate([FromBody]dynamic requestBody)
        {
            try
            {
                var templateList = appEngine.GetTemplateFromSearch(requestBody.Data.searchKey.Value);
                return SetSuccessResponce(templateList);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic GetAllTopics([FromBody]dynamic requestBody)
        {
            try
            {
                var templateTopicList = appEngine.GetAllTopics();
                return SetSuccessResponce(templateTopicList);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic GetTopicsInCategory([FromBody]dynamic requestBody)
        {
            try
            {
                var templateTopicList = appEngine.GetTopicsInCategory(requestBody.Data.CategoryName.Value);
                return SetSuccessResponce(templateTopicList);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }
    }
}