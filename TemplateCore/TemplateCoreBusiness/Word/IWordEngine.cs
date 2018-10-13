using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Word
{
    public interface IWordEngine
    {
        string CreateTemplateInWord(string iTemlateContent, string iTamplateName = null);
    }
}