using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Engine
{
    public interface IAppEngine
    {
        string GenerateTemplate();
        TemplateFormation GetTemplate(string iTemplateName);
        List<string> GetTemplateFromSearch(string iSearchKey);
        string CreateNewTemplate(string iData, string iTemplateName, string iUserEmail);
        string CreateNewTopic(string i_categoryName, string i_headerName);
        string UpdateHeaderInTopic(string i_categoryName, string i_oldHeaderName, string i_newHeaderName);
        string DeleteTopic(string i_categoryName, string i_headerName);
        string DeleteTemplate(string templateName);
        List<string> getTopicsInCategory(string i_categoryName);
        List<TopicEntity> getAllTopics();
    }
}
