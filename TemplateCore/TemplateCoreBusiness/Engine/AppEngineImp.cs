using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Database;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Engine
{
    public class AppEngineImp : IAppEngine
    {
        public string CreateNewTemplate(string iData, string iTemplateName, string iUserEmail)
        {
            try
            {
                TemplateEntity templateEntity = creatNewTemplateEntity(iData, iTemplateName, iUserEmail);
                return DataBaseFactory.GetDbInstance().CreateNewTemplate(templateEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create the new template: {ex.Message}");
            }
            
        }

        public string GenerateTemplate()
        {
            throw new NotImplementedException();
        }

        public TemplateFormation GetTemplate(string iTemplateName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTemplateFromSearch(string iSearchKey)
        {
            return DataBaseFactory.GetDbInstance().SearchTemplate(iSearchKey);
        }

        public string CreateNewTopic(string iCategoryName, string iHeaderName)
        {
            try
            {
                TopicEntity topicEntity = new TopicEntity(iCategoryName, iHeaderName);
                return DataBaseFactory.GetDbInstance().CreateNewTopic(topicEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create the new topic: {ex.Message}");
            }
        }

        public string UpdateHeaderInTopic(string iCategoryName, string iOldHeaderName, string iNewHeaderName)
        {
            try
            {
                TopicEntity topicEntity = new TopicEntity(iCategoryName, iOldHeaderName);
                return DataBaseFactory.GetDbInstance().UpdateHeaderInTopic(topicEntity, iNewHeaderName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update the new topic {iCategoryName}, Exception: {ex.Message}");
            }
        }

        public string DeleteTopic(string iCategoryName, string iHeaderName)
        {
            try
            {
                return DataBaseFactory.GetDbInstance().DeleteTopic(new TopicEntity(iCategoryName, iHeaderName));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete the new topic {iCategoryName} + {iHeaderName}, Exception: {ex.Message}");
            }
        }

        public string DeleteTemplate(string templateName)
        {
            try
            {
                return DataBaseFactory.GetDbInstance().DeleteTemplate(templateName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete the new template {templateName},\n Exception: {ex.Message}");
            }
        }

        public List<string> GetTopicsInCategory(string iCategoryName)
        {
            return DataBaseFactory.GetDbInstance().GetTopicsInCategory(iCategoryName);
        }

        public List<string> GetTopicsNames()
        {
            return DataBaseFactory.GetDbInstance().GetTopicsNames();
        }

        public List<TopicEntity> GetAllTopics()
        {
            return DataBaseFactory.GetDbInstance().GetAllTopics();
        }

        private TemplateEntity creatNewTemplateEntity(string iData, string iTemplateName, string iUserEmail)
        {
            TemplateEntity retVal = new TemplateEntity();
            retVal.TemplateJsonRow = iData;
            retVal.HeadName = iTemplateName;
            retVal.UserIdentity = iUserEmail;
            retVal.RateCounter = 0;
            retVal.Comments = "";

            return retVal;
        }
    }
}
