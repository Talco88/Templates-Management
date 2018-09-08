using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Engine
{
    public class UserEngienTestToDelete : IUserEngine
    {
        public UserEntity GetUserData(string iUserName)
        {
            throw new NotImplementedException();
        }

        public bool IsLogedOn(string iUserName)
        {
            throw new NotImplementedException();
        }

        public UserEntity LogInUser(string iUserName, string pass)
        {
            throw new NotImplementedException();
        }

        public bool LogOut(string iUserName)
        {
            throw new NotImplementedException();
        }

        public UserEntity RegisterNewUser(string iUserName, string pass)
        {
            throw new NotImplementedException();
        }
    }
}
