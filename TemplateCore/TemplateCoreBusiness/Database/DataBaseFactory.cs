using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Database
{
    public class DataBaseFactory
    {
        public static IDataBase GetDbInstance()
        {
            return DbTempImp.GetInstance;
        }
    }
}
