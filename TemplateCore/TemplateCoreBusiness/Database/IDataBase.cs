using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Database
{
    public interface IDataBase
    {
        string CreateNewTemplate(object[] templateValues);
        void SearchTemplate();
        string CreateNewUser(object[] userValues);
        Dictionary<string, object> GetUser(int id);
        string DeleteTemplate(int templateId);
    }
}
