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
                var template = appEngine.GetTemplate(requestBody.Data.CategoryName.Value, requestBody.Data.HeaderName.Value);
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

        [HttpPost]
        public dynamic RateTamplate([FromBody]dynamic requestBody)
        {
            try
            {
                var rateResponce = appEngine.RateTamplate(requestBody.Data.CategoryName.Value, requestBody.Data.TemplateName.Value, requestBody.Data.RateNumber.Value);
                return SetSuccessResponce(rateResponce);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic AddCommentToTemplate([FromBody]dynamic requestBody)
        {
            try
            {
                var addComentResponce = appEngine.AddCommentToTemplate(requestBody.Data.CategoryName.Value, requestBody.Data.TemplateName.Value, HttpContext.Current.User.Identity.Name, requestBody.Data.Comment.Value);
                return SetSuccessResponce(addComentResponce);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic SetSharedTemplate([FromBody]dynamic requestBody)
        {
            try
            {
                var setSheredResponce = appEngine.SetSharedTemplate(requestBody.Data.CategoryName.Value, requestBody.Data.TemplateName.Value, HttpContext.Current.User.Identity.Name, requestBody.Data.IsShared.Value);
                return SetSuccessResponce(setSheredResponce);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic DeleteTemplate([FromBody]dynamic requestBody)
        {
            try
            {
                var deleteResponce = appEngine.DeleteTemplate(requestBody.Data.CategoryName.Value, requestBody.Data.TemplateName.Value, HttpContext.Current.User.Identity.Name);
                return SetSuccessResponce(deleteResponce);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic MarkTemplateAsFavorite([FromBody]dynamic requestBody)
        {
            try
            {
                var favoritResponce = appEngine.MarkTemplateAsFavorite(requestBody.Data.CategoryName.Value, requestBody.Data.TemplateName.Value, HttpContext.Current.User.Identity.Name);
                return SetSuccessResponce(favoritResponce);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic RemoveMarkTemplateAsFavorite([FromBody]dynamic requestBody)
        {
            try
            {
                var favoritResponce = appEngine.RemoveMarkTemplateAsFavorite(requestBody.Data.CategoryName.Value, requestBody.Data.TemplateName.Value, HttpContext.Current.User.Identity.Name);
                return SetSuccessResponce(favoritResponce);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic GenerateHTMLTemplateWithValues([FromBody]dynamic requestBody)
        {
            try
            {
                TemplateFormation templateFormation = new TemplateFormation();
                templateFormation.HeaderName = requestBody.Data.Template.HeaderName;
                templateFormation.CategoryName = requestBody.Data.Template.CategoryName;
                List<WebDataContainer> list = new List<WebDataContainer>();
                foreach(var item in requestBody.Data.Template.Values)
                {
                    WebDataContainer webDataContainer = new WebDataContainer();
                    webDataContainer.Name = item.Name;
                    webDataContainer.Value = item.Value;
                    list.Add(webDataContainer);
                }
                templateFormation.Values = list;
                var generatedTemplate = appEngine.GenerateHTMLTemplateWithValues(templateFormation);
                return SetSuccessResponce(generatedTemplate);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic OpenTemplateInWord([FromBody]dynamic requestBody)
        {
            try
            {
                string templateContent = requestBody.Data.Content;
                string fileName = requestBody.Data.FileName;

                var generatedTemplate = appEngine.OpenTemplateInWord(templateContent, fileName);
                return SetSuccessResponce(generatedTemplate);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public dynamic AdminStuff([FromBody]dynamic requestBody)
        {
            try
            {
                return SetSuccessResponce("you are admin!");
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }
    }
}