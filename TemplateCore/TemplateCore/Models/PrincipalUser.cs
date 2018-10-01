using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TemplateCore.Models
{
    public class PrincipalUser : IPrincipal
    {
        private IdentityUser IdentityUser { get; }

        public PrincipalUser(IdentityUser iIdentityUser)
        {
            IdentityUser = iIdentityUser;
        }

        public bool IsInRole(string role)
        {
            return (role == IdentityUser.AuthenticationType);
        }

        public IIdentity Identity { get { return IdentityUser; } }
    }

    public class IdentityUser : IIdentity
    {
        public string Name { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }

        public IdentityUser(string iName, bool iIsAdmin)
        {
            Name = iName;
            IsAuthenticated = true;
            AuthenticationType = (iIsAdmin) ? "Administrators" : "users";
        }
    }
} 