using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Engine
{
    public interface IUserEngine
    {
        UserEntity GetUserData(string iUserName);
        UserEntity LogInUser(string iUserName, string pass);
        bool IsLogedOn(string iUserName);
        UserEntity RegisterNewUser(string iUserName, string pass);
        bool LogOut(string iUserName);
    }
}
