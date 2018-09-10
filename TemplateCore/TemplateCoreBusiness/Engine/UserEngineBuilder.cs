using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Engine
{
    public static class UserEngineBuilder
    {
        public static IUserEngine GetUserEngine()
        {
            return new UserEngineImp();
        }
    }
}
