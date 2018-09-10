using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Database
{
    public interface IDataBase
    {
        string CreateNewTemplate(TemplateEntity templateEntity);
        List<string> SearchTemplate(string iSearchKey);
        void CreateNewUser(UserEntity userEntity);
        UserEntity GetUser(string iEmail);
        string DeleteTemplate(string templateName);
    }
}
