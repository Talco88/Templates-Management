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
        string UpdateHeaderInTopic(TopicEntity topicEntity, string i_newHeaderName);
        string DeleteTopic(TopicEntity topicEntity);
        List<string> getTopicsInCategory(string i_categoryName);
        List<TopicEntity> getAllTopics();
    }
}
