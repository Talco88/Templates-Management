using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Database
{
    public interface IDataBase
    {
        string CreateNewTemplate(TemplateEntity templateEntity);
        List<string> SearchTemplate(string iSearchKey);
        void CreateNewUser(UserEntity userEntity);
        UserEntity GetUser(string iEmail);
        string DeleteTemplate(string templateName);
        string CreateNewTopic(TopicEntity topicEntity);
        string UpdateHeaderInTopic(TopicEntity topicEntity, string iNewHeaderName);
        string DeleteTopic(TopicEntity topicEntity);
        List<string> GetTopicsInCategory(string iCategoryName);
        List<string> GetTopicsNames();
        List<TopicEntity> GetAllTopics();
        bool IsTopicExistInCategory(string iCategoryName, string iHeaderName);
        TemplateEntity GetTemplateEntity(string iCategoryName, string iTemplateName);
        string UpdateTemplate(TemplateEntity iTemplateEntity);
        string UpdateUser(UserEntity iUserEntity);

        [Obsolete]
        string DeleteAllTable(string iTableName);
    }
}
