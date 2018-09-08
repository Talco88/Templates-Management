using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Engine
{
    public class SomeAppEngineToDelete : IAppEngine
    {
        public void CreateNewTemplate(GenerateNewTemplateEntity iNewTemplateData)
        {
            throw new NotImplementedException();
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
    }
}
