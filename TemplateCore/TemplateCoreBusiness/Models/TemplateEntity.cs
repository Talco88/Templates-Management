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
        public string Category { get; set; } = "כללי";
        public string HeadName { get; set; }
        public string TemplateJsonRow { get; set; }
        public string UserIdentity { get; set; }
        public string Comments { get; set; } = "";
        public bool IsShared { get; set; } = false;
        public int Rate { get; set; } = 0;
        public int RateCounter { get; set; } = 0;
        public int RateSum { get; set; } = 0;


        private void AddRateCounter()
        {
            RateCounter++;
        }

        private void AddRateSum(int iNumber)
        {
            RateSum += iNumber;
        }

        public void AddRate(int iNewRate)
        {
            AddRateCounter();
            if(RateCounter > 1)
            {
                AddRateSum(iNewRate);
            }
            
            Rate = (RateSum + iNewRate) / RateCounter;
        }

        public void AddComment(string iNewComment)
        {
            Comments = Common.CommonUtilities.AddStringToStringWithSeparate(Comments, iNewComment, '|');
        }

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