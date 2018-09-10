﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Engine
{
    public class SomeAppEngineToDelete : IAppEngine
    {
        public void CreateNewTemplate(string iData, string iTemplateName, string iUserEmail)
        {
            throw new NotImplementedException();
        }

        public string GenerateTemplate()
        {
            return "Generated";
        }

        public TemplateFormation GetTemplate(string iTemplateName)
        {
            return new TemplateFormation()
            {
                Name = "mocTemplate",
                Propertys = new List<WebDataContainer>() {
                    new WebDataContainer() { Name = "Field1" },
                    new WebDataContainer() { Name = "Field2" },
                    new WebDataContainer() { Name = "Field3" }
                }
            };
        }

        public List<string> GetTemplateFromSearch(string iSearchKey)
        {
            return new List<string>() { "template1", "template2", "template3", "template4" };
        }
    }
}
