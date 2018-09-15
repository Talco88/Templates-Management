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

        public string CreateNewTopic(string i_categoryName, string i_headerName)
        {
            try
            {
                TopicEntity topicEntity = new TopicEntity(i_categoryName, i_headerName);
                return DataBaseFactory.GetDbInstance().CreateNewTopic(topicEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create the new topic: {ex.Message}");
            }
        }

        public string UpdateHeaderInTopic(string i_categoryName, string i_oldHeaderName, string i_newHeaderName)
        {
            try
            {
                TopicEntity topicEntity = new TopicEntity(i_categoryName, i_oldHeaderName);
                return DataBaseFactory.GetDbInstance().UpdateHeaderInTopic(topicEntity, i_newHeaderName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update the new topic {i_categoryName}, Exception: {ex.Message}");
            }
        }

        public string DeleteTopic(string i_categoryName, string i_headerName)
        {
            try
            {
                return DataBaseFactory.GetDbInstance().DeleteTopic(new TopicEntity(i_categoryName, i_headerName));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete the new topic {i_categoryName} + {i_headerName}, Exception: {ex.Message}");
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
            return DataBaseFactory.GetDbInstance().getTopicsInCategory(iCategoryName);
        }

        public List<TopicEntity> GetAllTopics()
        {
            return DataBaseFactory.GetDbInstance().getAllTopics();
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
