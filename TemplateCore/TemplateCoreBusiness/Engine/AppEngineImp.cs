using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Database;
using TemplateCoreBusiness.Models;
using Xceed.Words.NET;

namespace TemplateCoreBusiness.Engine
{
    public class AppEngineImp : IAppEngine
    {
        public string CreateNewTemplate(string iData, string iTemplateName, string iUserEmail, string iCategory,
            bool isShared = false)
        {
            try
            {
                TemplateEntity templateEntity =
                    creatNewTemplateEntity(iData, iTemplateName, iUserEmail, iCategory, isShared);
                if (isTemplateExistInDb(templateEntity.Category, templateEntity.HeadName))
                {
                    throw new Exception(
                        $"The template in Category {templateEntity.Category} with name {templateEntity.HeadName} is already exist");
                }
                else
                {
                    DataBaseFactory.GetDbInstance()
                        .CreateNewTopic(new TopicEntity(templateEntity.Category, templateEntity.HeadName));
                    return DataBaseFactory.GetDbInstance().CreateNewTemplate(templateEntity);
                }
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
                throw new Exception(
                    $"Failed to delete the new topic {iCategoryName} + {iHeaderName}, Exception: {ex.Message}");
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

        public string RateTamplate(string iCategoryName, string iTemplateName, int iRateNumber)
        {
            try
            {
                TemplateEntity templateEntity =
                    DataBaseFactory.GetDbInstance().GetTemplateEntity(iCategoryName, iTemplateName);
                templateEntity.AddRate(iRateNumber);
                return DataBaseFactory.GetDbInstance().UpdateTemplate(templateEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to rate template: {ex.Message}");
            }
        }

        public string AddCommentToTemplate(string iCategoryName, string iTemplateName, string iUserEmail, string iComment)
        {
            try
            {
                TemplateEntity templateEntity =
                    DataBaseFactory.GetDbInstance().GetTemplateEntity(iCategoryName, iTemplateName);
                templateEntity.AddComment($"{iUserEmail} :{iComment}");
                return DataBaseFactory.GetDbInstance().UpdateTemplate(templateEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to rate template: {ex.Message}");
            }
        }

        public string SetSharedTemplate(string iTamplateName, string iUserEmail, bool isShared)
        {
            throw new NotImplementedException();
        }

        public string InsertValuesToTemplate(string iTamplateName, string iValues)
        {
            throw new NotImplementedException();
        }

        public string DeleteTemplate(string iTamplateName, string iUserEmail)
        {
            throw new NotImplementedException();
        }

        public string MarkTemplateAsFavorite(string iCategoryName, string iTemplateName, string iUserEmail)
        {
            try
            {
                UserEntity userEntity = DataBaseFactory.GetDbInstance().GetUser(iUserEmail);
                string templateName = $"{iCategoryName}:{iTemplateName}";
                string[] favoriteTemplates = getListFavoriteTemplatesFromString(userEntity.FavoriteTemplates);
                if (isTemplateExistsInFavoriteList(favoriteTemplates, templateName) == false)
                {
                    userEntity.AddFavoriteTemplate(templateName);
                }
                
                return DataBaseFactory.GetDbInstance().UpdateUser(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to mark template as favorite: {ex.Message}");
            }
        }

        public string RemoveMarkTemplateAsFavorite(string iCategoryName, string iTemplateName, string iUserEmail)
        {
            throw new NotImplementedException();
        }

        public DocX OpenTemplateInWord(string iTamplateName, string iTemlateContent)
        {
            throw new NotImplementedException();
        }

        private TemplateEntity creatNewTemplateEntity(string iData, string iTemplateName, string iUserEmail,
            string iCategory, bool isShared)
        {
            TemplateEntity retVal = new TemplateEntity();
            retVal.TemplateJsonRow = iData;
            retVal.HeadName = iTemplateName;
            retVal.UserIdentity = iUserEmail;
            retVal.Category = iCategory;
            retVal.IsShared = isShared;

            return retVal;
        }

        private bool isTemplateExistInDb(string iCategoryName, string iHeaderName)
        {
            return DataBaseFactory.GetDbInstance().IsTopicExistInCategory(iCategoryName, iHeaderName);
        }

        private string[] getListFavoriteTemplatesFromString(string iList)
        {
            return iList.Split('|');
        }

        private bool isTemplateExistsInFavoriteList(string[] iList, string iTemplateName)
        {
            return iList.Contains(iTemplateName);
        }
    }
}