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
        public void CreateNewTemplate(string iData, string iTemplateName, string iUserEmail)
        {
            try
            {
                TemplateEntity templateEntity = creatNewTemplateEntity(iData, iTemplateName, iUserEmail);
                DataBaseFactory.GetDbInstance().CreateNewTemplate(templateEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create the new template:\n" + ex.Message);
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
            throw new NotImplementedException();
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
