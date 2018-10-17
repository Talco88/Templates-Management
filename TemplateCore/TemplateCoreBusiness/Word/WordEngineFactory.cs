using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Word
{
    public static class WordEngineFactory
    {
        private static WordEngineImp m_instance = null;
        private static readonly object m_padlock = new object();

        public static IWordEngine GetWordEngineInstance()
        {
            if (m_instance == null)
            {
                lock (m_padlock)
                {
                    if (m_instance == null)
                    {
                        m_instance = new WordEngineImp();
                    }
                }
            }

            return m_instance;
        }
    }
}
