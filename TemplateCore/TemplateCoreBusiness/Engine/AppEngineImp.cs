using System;
using System.Collections.Generic;
using System.Linq;
using TemplateCoreBusiness.Database;
using TemplateCoreBusiness.Models;
using TemplateCoreBusiness.Word;

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

        public TemplateFormation GetTemplate(string iCategoryName, string iTemplateName)
        {
            try
            {
                return getTemplateFromDB(iCategoryName, iTemplateName);
            }
            catch (Exception e)
            {
                throw new Exception($"Error during GetTemplate: {e.Message}");
            }
        }

        public List<string> GetTemplateFromSearch(string iSearchKey, bool isAdmin = false, string iUserEmail = "")
        {
            return DataBaseFactory.GetDbInstance().SearchTemplate(iSearchKey, isAdmin, iUserEmail);
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

        public string AddCommentToTemplate(string iCategoryName, string iTemplateName, string iUserEmail,
            string iComment)
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

        public string SetSharedTemplate(string iCategoryName, string iTemplateName, string iUserEmail, bool isShared)
        {
            try
            {
                UserEntity userEntity = DataBaseFactory.GetDbInstance().GetUser(iUserEmail);
                TemplateEntity templateEntity =
                    DataBaseFactory.GetDbInstance().GetTemplateEntity(iCategoryName, iTemplateName);
                if (isAuthorizeToUpdateTemplate(templateEntity, userEntity))
                {
                    templateEntity.IsShared = isShared;
                }
                else
                {
                    throw new Exception(
                        $"The User {userEntity.Email} has no authority to set share template {templateEntity.Category}:{templateEntity.HeadName}");
                }

                return DataBaseFactory.GetDbInstance().UpdateTemplate(templateEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set shared template: {ex.Message}");
            }
        }

        public string DeleteTemplate(string iCategoryName, string iTemplateName, string iUserEmail)
        {
            try
            {
                UserEntity userEntity = DataBaseFactory.GetDbInstance().GetUser(iUserEmail);
                TemplateEntity templateEntity =
                    DataBaseFactory.GetDbInstance().GetTemplateEntity(iCategoryName, iTemplateName);
                if (isAuthorizeToUpdateTemplate(templateEntity, userEntity))
                {
                    DataBaseFactory.GetDbInstance().DeleteTopic(new TopicEntity(iCategoryName, iTemplateName));
                    return DataBaseFactory.GetDbInstance().DeleteTemplate(iCategoryName, iTemplateName);
                }
                else
                {
                    throw new Exception(
                        $"The User {userEntity.Email} has no authority to delete the template {templateEntity.Category}:{templateEntity.HeadName}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete the template: {ex.Message}");
            }
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
            try
            {
                UserEntity userEntity = DataBaseFactory.GetDbInstance().GetUser(iUserEmail);
                string templateName = $"{iCategoryName}:{iTemplateName}";
                string[] oldFavoriteTemplates = getListFavoriteTemplatesFromString(userEntity.FavoriteTemplates);
                string[] newFavoriteTemplatesArray = removeTemplateFromList(oldFavoriteTemplates, templateName);
                string newFavoriteTemplateString = buildFavoriteTemplatesStringFromArray(newFavoriteTemplatesArray);
                userEntity.FavoriteTemplates = newFavoriteTemplateString;
                return DataBaseFactory.GetDbInstance().UpdateUser(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to mark template as favorite: {ex.Message}");
            }
        }
        
        public string OpenTemplateInWord(string iTamplateName, string iTemlateContent)
        {
            try
            {
                return WordEngineFactory.GetDbInstance().CreateTemplateInWord(iTamplateName, iTemlateContent);
            }
            catch (Exception e)
            {
                throw new Exception("Error during OpenTemplateInWord: " + e.Message);
            }
        }

        public string GenerateHTMLTemplateWithValues(TemplateFormation iTemplate)
        {
            try
            {
                TemplateEntity templateEntity = DataBaseFactory.GetDbInstance().GetTemplateEntity(iTemplate.CategoryName, iTemplate.HeaderName);
                Dictionary<string, object> templateData =  templateEntity.TemplateData;
                string template = switchValuesInTemplate(templateData["Template"].ToString(), iTemplate.Values);

                return template;
            }
            catch (Exception e)
            {
                throw new Exception("Error during GenerateHTMLTemplateWithValues: " + e.Message);
            }
        }

        private string switchValuesInTemplate(string iTemplate, List<WebDataContainer> iValues)
        {
            for (int i = 0; i < iValues.Count; i++)
            {
                iTemplate = iTemplate.Replace($"${iValues[i].Name}", iValues[i].Value);
            }

            return iTemplate;
        }

        private TemplateFormation getTemplateFromDB(string iCategoryName, string iTemplateName)
        {
            TemplateEntity templateEntity = DataBaseFactory.GetDbInstance().GetTemplateEntity(iCategoryName, iTemplateName);
            Dictionary<string, object> templateData = templateEntity.TemplateData;
            List<string> fieldsNames = getListFieldsNamesFromTemplate(templateData["Template"].ToString());

            return createTemplateFormationWithValues(templateEntity.Category, templateEntity.HeadName, fieldsNames);
        }

        private TemplateFormation createTemplateFormationWithValues(string iCategoryName, string iTemplateName,
            List<string> iWebDataContainerValues)
        {
            TemplateFormation retVal = new TemplateFormation();
            retVal.CategoryName = iCategoryName;
            retVal.HeaderName = iTemplateName;
            retVal.Values = createWebDataContainerWithNames(iWebDataContainerValues);
            return retVal;
        }

        private List<WebDataContainer> createWebDataContainerWithNames(List<string> iWebDataContainerValues)
        {
            List<WebDataContainer> retVal = new List<WebDataContainer>();
            foreach (string item in iWebDataContainerValues)
            {
                WebDataContainer webDataContainer = new WebDataContainer();
                webDataContainer.Name = item;
                retVal.Add(webDataContainer);
            }

            return retVal;
        }

        private List<string> getListFieldsNamesFromTemplate(string iTemplateJson)
        {
            List<string> retVal = new List<string>();
            string[] splittedArray = iTemplateJson.Split('$');
            for (int i = 0; i < splittedArray.Length; i++)
            {
                if (i != 0)
                {
                    retVal.Add(splittedArray[i].Split(' ')[0]);
                }
            }

            return retVal;
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

        private string[] removeTemplateFromList(string[] iArrayStrings, string iTemplateName)
        {
            List<string> iList = iArrayStrings.ToList();
            iList.Remove(iTemplateName);
            return iList.ToArray();
        }

        private string buildFavoriteTemplatesStringFromArray(string[] iArrayStrings)
        {
            string retVal = "";

            foreach (string item in iArrayStrings)
            {
                retVal = Common.CommonUtilities.AddStringToStringWithSeparate(retVal, item, '|');
            }

            return retVal;
        }

        private bool isAuthorizeToUpdateTemplate(TemplateEntity iTemplateEntity, UserEntity iUserEntity)
        {
            return iUserEntity.IsAdmin || iTemplateEntity.UserIdentity.Equals(iUserEntity.Email);
        }
    }
}