using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Common
{
    public class CommonUtilities
    {
        public static string AddStringToStringWithSeparate(string iOldString, string iStringToAdd, char iSeparate)
        {
            string retVal = "";
            if (iOldString.Length == 0)
            {
                retVal = iStringToAdd;
            }
            else
            {
                retVal = $"{iOldString}{iSeparate}{iStringToAdd}";
            }

            return retVal;
        }
    }
}
