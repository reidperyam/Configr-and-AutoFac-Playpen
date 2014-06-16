using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Nancy.Security;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Nancy;
using Nancy.Responses;

namespace Core
{
    public class CatchModule : NancyModule
    {
        private const           string TenantIdClaimType = "f721817b-99eb-4505-b220-850208ab5dd7";
        private static readonly string AppPrincipalId    = "17c9a991-4954-48e4-9cf9-39b126ba975c";        // 'clientID' for NancyDemoWebClient AAD config
        private static readonly string AppKey            = "wTB9UZQGx+1dVcKPlsdGbVcHNnaiH6PQgKDdSvf0bak=";// 'key' from NancyDemoWebClient AAD config

        public CatchModule()
        {
            this.RequiresMSOwinAuthentication();

            Get["/Home/CatchCode"] = _ =>
            {
                AuthenticationContext ac = new AuthenticationContext(string.Format("https://login.windows.net/{0}",
                                              TenantIdClaimType));

                ClientCredential clcred = new ClientCredential(AppPrincipalId, AppKey);

                var ar = ac.AcquireTokenByAuthorizationCode(Request.Query.code,
                               new Uri("https://localhost:44308/Home/CatchCode"), clcred);

                return ar;
            };
        }
    }
}
