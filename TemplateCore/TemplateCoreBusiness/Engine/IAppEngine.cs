using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;
using Xceed.Words.NET;

namespace TemplateCoreBusiness.Engine
{
    public interface IAppEngine
    {
        string GenerateTemplate();
        TemplateFormation GetTemplate(string iCategoryName, string iTemplateName);
        TemplateEntity GetTemplateDetails(string iCategoryName, string iTemplateName);
        List<string> GetTemplateFromSearch(string iSearchKey, bool isAdmin = false, string iUserEmail = "");
        string CreateNewTemplate(string iData, string iTemplateName, string iUserEmail,string iCategory, bool isShared = false);
        string CreateNewTopic(string iCategoryName, string iHeaderName);
        string UpdateHeaderInTopic(string iCategoryName, string iOldHeaderName, string iNewHeaderName);
        string DeleteTopic(string iCategoryName, string iHeaderName);
        List<string> GetTopicsInCategory(string iCategoryName);
        List<string> GetTopicsNames();
        List<TopicEntity> GetAllTopics();
        string RateTamplate(string iCategoryName, string iTemplateName, int iRateNumber);
        string AddCommentToTemplate(string iCategoryName, string iTemplateName, string iUserEmail, string iComment);
        string SetSharedTemplate(string iCategoryName, string iTemplateName, string iUserEmail, bool isShared);
        string DeleteTemplate(string iCategoryName, string iTemplateName, string iUserEmail);
        string MarkTemplateAsFavorite(string iCategoryName, string iTemplateName, string iUserEmail);
        string RemoveMarkTemplateAsFavorite(string iCategoryName, string iTemplateName, string iUserEmail);
        string OpenTemplateInWord(string iTemlateContent, string iTamplateName = null);
        string GenerateHTMLTemplateWithValues(TemplateFormation iTemplate);
    }
}
