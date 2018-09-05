using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TemplateCoreBusiness.Models
{
    public class TemplateEntity
    {
        private Dictionary<string, object> _templateData = null;
        public string Name { get; set; }
        public string TemplateJsonRow { get; set; }
        public Dictionary<string, object> TemplateData
        {
            get
            {
                if (_templateData == null && !string.IsNullOrEmpty(TemplateJsonRow))
                {
                    _templateData = JsonConvert.DeserializeObject<Dictionary<string, object>>(TemplateJsonRow);
                }
                return _templateData;
            }
        }
    }
}
