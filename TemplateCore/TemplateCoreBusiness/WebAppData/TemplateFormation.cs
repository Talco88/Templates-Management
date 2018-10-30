using System.Collections.Generic;

namespace TemplateCoreBusiness.Engine
{
    public class TemplateFormation
    {
        public string HeaderName { get; set; }
        public string CategoryName { get; set; }
        public string UserIdentity { get; set; }
        public List<WebDataContainer> Values { get; set; }
    }
}