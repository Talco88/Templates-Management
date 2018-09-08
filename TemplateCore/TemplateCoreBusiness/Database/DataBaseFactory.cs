using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Database
{
    public class DataBaseFactory
    {
        private static DbTempImp m_instance = null;
        private static readonly object m_padlock = new object();

        public static IDataBase GetDbInstance()
        {
            if (m_instance == null)
            {
                lock (m_padlock)
                {
                    if (m_instance == null)
                    {
                        m_instance = new DbTempImp();
                    }
                }
            }

            return m_instance;
        }
    }
}