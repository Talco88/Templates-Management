using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Engine
{
    interface IAppEngine
    {
        string GenerateTemplate();
        string GetTemplate(string iTemplateName);
        List<string> GetTemplateFromSearch(string iSearchKey);
        void CreateNewTemplate(GenerateNewTemplateEntity iNewTemplateData);
    }
}
