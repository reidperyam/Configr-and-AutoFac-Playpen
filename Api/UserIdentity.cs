using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace SalesApplication.Api
{
    /// <summary>
    /// Model class for adapting ADAL's UserInfo class into an interface consumable by WebApi
    /// </summary>
    public class UserIdentity : IIdentity
    {
        private string _name;

        public UserIdentity()
        { }

        public UserIdentity(Microsoft.IdentityModel.Clients.ActiveDirectory.UserInfo userInfo)
        {
            _name = userInfo.UserId;
        }

        public string AuthenticationType
        {
            get { return "Bearer"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(_name); }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
