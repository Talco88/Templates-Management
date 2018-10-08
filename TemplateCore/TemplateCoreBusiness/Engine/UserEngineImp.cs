using System;
using TemplateCoreBusiness.Database;
using TemplateCoreBusiness.Models;

namespace TemplateCoreBusiness.Engine
{
    public class UserEngineImp : IUserEngine
    {
        public UserEntity GetUserData(string iEmail)
        {
            return DataBaseFactory.GetDbInstance().GetUser(iEmail);
        }

        public bool IsLogedOn(string iUserName)
        {
            throw new NotImplementedException();
        }

        public UserEntity LogInUser(string iEmail, string pass)
        {
            UserEntity retVal = GetUserData(iEmail);
            if (retVal.Password.Equals(pass))
            {
                return retVal;
            }
            else
            {
                throw new Exception($"The password for the email: {iEmail} is incorrect");
            }
        }

        public bool LogOut(string iUserName)
        {
            throw new NotImplementedException();
        }

        public UserEntity RegisterNewUser(string iUserFirstName, string iUserLastName, string iUserEmail, string pass, bool isAdmin = false)
        {
            try
            {
                UserEntity newUser = creatEntity(iUserFirstName, iUserLastName, iUserEmail, pass, isAdmin);
                DataBaseFactory.GetDbInstance().CreateNewUser(newUser);
                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to register the new user:\n" + ex.Message);
            }
        }

        private UserEntity creatEntity(string iUserFirstName, string iUserLastName, string iUserEmail, string pass, bool isAdmin)
        {
            UserEntity retValEntity = new UserEntity();
            retValEntity.FirstName = iUserFirstName;
            retValEntity.LastName = iUserLastName;
            retValEntity.Password = pass;
            retValEntity.Email = iUserEmail;
            retValEntity.CreationTime = DateTime.Now;
            retValEntity.IsAdmin = isAdmin;

            return retValEntity;
        }
    }
}
