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
        string CreateNewTemplate(object[] templateValues);
        void SearchTemplate();
        void CreateNewUser(UserEntity userEntity);
        UserEntity GetUser(string iEmail);
        string DeleteTemplate(int templateId);
    }
}
