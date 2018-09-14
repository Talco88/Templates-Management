using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Engine
{
    public class SomeAppEngineToDelete : IAppEngine
    {
        public string CreateNewTemplate(string iData, string iTemplateName, string iUserEmail)
        {
            throw new NotImplementedException();
        }

        public string CreateNewTopic(string i_categoryName, string i_headerName)
        {
            throw new NotImplementedException();
        }

        public string UpdateHeaderInTopic(string i_categoryName, string i_oldHeaderName, string i_newHeaderName)
        {
            throw new NotImplementedException();
        }

        public string DeleteTopic(string i_categoryName, string i_headerName)
        {
            throw new NotImplementedException();
        }

        public string DeleteTemplate(string templateName)
        {
            throw new NotImplementedException();
        }

        public List<string> getTopicsInCategory(string i_categoryName)
        {
            throw new NotImplementedException();
        }

        public List<TopicEntity> getAllTopics()
        {
            throw new NotImplementedException();
        }

        public string GenerateTemplate()
        {
            return "Generated";
        }

        public TemplateFormation GetTemplate(string iTemplateName)
        {
            return new TemplateFormation()
            {
                Name = "mocTemplate",
                Propertys = new List<WebDataContainer>() {
                    new WebDataContainer() { Name = "Field1" },
                    new WebDataContainer() { Name = "Field2" },
                    new WebDataContainer() { Name = "Field3" }
                }
            };
        }

        public List<string> GetTemplateFromSearch(string iSearchKey)
        {
            return new List<string>() { "template1", "template2", "template3", "template4" };
        }
    }
}
