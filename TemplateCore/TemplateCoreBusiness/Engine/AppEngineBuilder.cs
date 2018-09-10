using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Engine
{
    public static class AppEngineBuilder
    {
        public static IAppEngine GetAppEngine()
        {
            return new AppEngineImp();
        }
    }
}
