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
        private int _rate = 0;
        private int _rateCounter = 0;
        public string Category { get; set; } = "כללי";
        public string HeadName { get; set; }
        public string TemplateJsonRow { get; set; }
        public string UserIdentity { get; set; }
        public string Comments { get; set; }

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

        public int RateCounter
        {
            get { return _rateCounter; }
            set { _rateCounter++; }
        }

        public int Rate
        {
            get { return _rate; }
            set
            {
                RateCounter = RateCounter;
                _rate = (value + _rate) / RateCounter;
            }
        }
    }
}
