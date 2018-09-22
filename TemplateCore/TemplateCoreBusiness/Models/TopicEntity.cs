using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Models
{
    public class TopicEntity
    {
        public string Category { get; }
        public string Header { get; set; }

        public TopicEntity(string i_categoryName, string i_headerName)
        {
            Category = i_categoryName;
            Header = i_headerName;
        }
    }
}
