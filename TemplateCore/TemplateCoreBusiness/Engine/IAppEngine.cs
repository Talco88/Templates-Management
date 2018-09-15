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
        string CreateNewTopic(string iCategoryName, string iHeaderName);
        string UpdateHeaderInTopic(string iCategoryName, string iOldHeaderName, string iNewHeaderName);
        string DeleteTopic(string iCategoryName, string iHeaderName);
        string DeleteTemplate(string templateName);
        List<string> GetTopicsInCategory(string iCategoryName);
        List<string> GetTopicsNames();
        List<TopicEntity> GetAllTopics();
    }
}
