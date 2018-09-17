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
        UserEntity GetUserData(string iEmail);
        UserEntity LogInUser(string iEmail, string pass);
        bool IsLogedOn(string iUserName);
        UserEntity RegisterNewUser(string iUserFirstName, string iUserLastName, string iUserEmail, string pass, bool isAdmin = false);
        bool LogOut(string iUserName);
    }
}