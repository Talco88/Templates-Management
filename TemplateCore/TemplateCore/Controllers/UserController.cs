using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TemplateCore.Models;
using TemplateCoreBusiness.Engine;
using TemplateCoreBusiness.Models;

namespace TemplateCore.Controllers
{
    public class UserController : BaseController
    {
        private readonly string SESSION_USER_EMAIL = "UserMail";
        protected IUserEngine userEngine { get; }

        public UserController()
        {
            userEngine = UserEngineBuilder.GetUserEngine();
        }

        [HttpPost]
        public dynamic Login([FromBody]dynamic requestBody)
        {
            
            try
            {
                UserEntity user = userEngine.LogInUser(
                    requestBody.Data.Email.Value, 
                    requestBody.Data.Password.Value
                );

                SetPrincipal(user.Email, user.IsAdmin);
                return SetSuccessResponce(user);
            }
            catch(Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic GetUserData([FromBody]dynamic requestBody)
        {
            try
            {
                var user = userEngine.GetUserData(requestBody.Data.Email.Value);
                return SetSuccessResponce(user);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic RegisterNewUser([FromBody]dynamic requestBody)
        {
            try
            {
                var user = userEngine.RegisterNewUser(
                    requestBody.Data.FirstName.Value,
                    requestBody.Data.LastName.Value,
                    requestBody.Data.Email.Value,
                    requestBody.Data.Password.Value
                );

                return SetSuccessResponce(user);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic IsLogin([FromBody] dynamic requestBody)
        {
            try
            {
                var user = userEngine.IsLogedOn(requestBody.Data.Email.Value);
                HttpContext.Current.Session[SESSION_USER_EMAIL] = null;
                return SetSuccessResponce(user);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }

        [HttpPost]
        public dynamic Logout([FromBody] dynamic requestBody)
        {
            try
            {
                var user = userEngine.LogOut(requestBody.Data.Email.Value);
                return SetSuccessResponce(user);
            }
            catch (Exception ex)
            {
                return SetExceptionResponce(ex);
            }
        }
    }
}