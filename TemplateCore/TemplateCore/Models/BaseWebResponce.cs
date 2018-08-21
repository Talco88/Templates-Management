using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateCore.Models
{
    public class BaseWebResponce<T>
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public T RetObject { get; set; }
    }
}