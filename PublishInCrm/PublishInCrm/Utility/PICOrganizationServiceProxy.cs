using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CemYabansu.PublishInCrm.Utility
{
    class PICOrganizationServiceProxy : OrganizationServiceProxy
    {
        private Guid id = Guid.NewGuid();
        private PICOrganizationServiceFactory factory;
        internal PICOrganizationServiceFactory Factory
        {
            get
            {
                return factory;
            }

            set
            {
                factory = value;
            }
        }

        internal Guid Id
        {
            get
            {
                return id;
            }
        }

        public PICOrganizationServiceProxy(IServiceConfiguration<IOrganizationService> serviceConfiguration, ClientCredentials clientCredentials) : base(serviceConfiguration, clientCredentials)
        {
        }

        public PICOrganizationServiceProxy(IServiceManagement<IOrganizationService> serviceManagement, ClientCredentials clientCredentials) : base(serviceManagement, clientCredentials)
        {
        }

        public PICOrganizationServiceProxy(IServiceManagement<IOrganizationService> serviceManagement, SecurityTokenResponse securityTokenResponse) : base(serviceManagement, securityTokenResponse)
        {
        }

        public PICOrganizationServiceProxy(IServiceConfiguration<IOrganizationService> serviceConfiguration, SecurityTokenResponse securityTokenResponse) : base(serviceConfiguration, securityTokenResponse)
        {
        }

        public PICOrganizationServiceProxy(Uri uri, Uri homeRealmUri, ClientCredentials clientCredentials, ClientCredentials deviceCredentials) : base(uri, homeRealmUri, clientCredentials, deviceCredentials)
        {

        }

        protected override void Dispose(bool disposing)
        {
            factory.proxies.Remove(id);
            base.Dispose(disposing);
        }
    }
}
