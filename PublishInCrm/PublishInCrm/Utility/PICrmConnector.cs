using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CemYabansu.PublishInCrm.Utility
{
    static class PICrmConnector
    {
        public static bool Test(CRMCredentials credentials)
        {
            using (var factory = new PICOrganizationServiceFactory(credentials.OrganizationServiceUrl, credentials))
            {
                factory.SetEnableProxies(true);
                var service = factory.CreateOrganizationService(null);
                service.Execute(new WhoAmIRequest());
                return true;
            }

            return false;
        }
    }
}
