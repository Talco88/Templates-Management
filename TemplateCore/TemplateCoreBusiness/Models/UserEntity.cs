﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Common;

namespace TemplateCoreBusiness.Models
{
    public class UserEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string FavoriteTemplates { get; set; } = "";

        public void AddFavoriteTemplate(string iNewTemplate)
        {
            FavoriteTemplates = Common.CommonUtilities.AddStringToStringWithSeparate(FavoriteTemplates, iNewTemplate, '|');
        }
    }
}
