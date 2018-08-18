using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Database
{
    public interface IDataBase
    {
        void CreateNewTemplate();
        void SearchTemplate();
        void CreateNewUser();
        void GetUser();

    }
}
