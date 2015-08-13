using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CemYabansu.PublishInCrm.Utility
{
    class PICOrganizationServiceFactory : IOrganizationServiceFactory, IDisposable
    {
        private static PICOrganizationServiceFactory currentUserFactory = null;

        private readonly object locker = new object();
        private IServiceManagement<IOrganizationService> organizationServiceMetadata = null;
        private readonly string organizationServiceUrl;
        private AuthenticationCredentials authenticationCredentials;
        internal Dictionary<Guid, PICOrganizationServiceProxy> proxies = new Dictionary<Guid, PICOrganizationServiceProxy>();
        private DateTime tokenExpiredTime;
        private int minutePadding = 10;
        private CRMCredentials crmCredentials;
        private bool enableProxies = false;

        public void SetEnableProxies(bool enable)
        {
            enableProxies = enable;
        }

        public PICOrganizationServiceFactory(string organizationServiceUrl, CRMCredentials credentials)
        {
            if (string.IsNullOrEmpty(organizationServiceUrl))
            {
                throw new ArgumentNullException("organizationServiceUrl");
            }

            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            crmCredentials = credentials;
            this.organizationServiceUrl = organizationServiceUrl;
            DownloadMetadata();
            Authenticate(credentials.Username, credentials.Password, credentials.Domain);

        }

        public void Refresh(CRMCredentials credentials)
        {
            DownloadMetadata();
            Authenticate(credentials.Username, credentials.Password, credentials.Domain);
        }

        public OrganizationServiceProxy CreateProxy()
        {
            lock (locker)
            {
                PICOrganizationServiceProxy crmProxy;


                if (authenticationCredentials.SecurityTokenResponse == null) //Active Directory
                {
                    crmProxy = new PICOrganizationServiceProxy(organizationServiceMetadata, authenticationCredentials.ClientCredentials);
                }
                else //Federated
                {
                    if (DateTime.Now.AddMinutes(minutePadding) > tokenExpiredTime)
                    {
                        Authenticate(crmCredentials.Username, crmCredentials.Password, crmCredentials.Domain);
                    }

                    crmProxy = new PICOrganizationServiceProxy(organizationServiceMetadata, authenticationCredentials.SecurityTokenResponse);
                }

                if (enableProxies)
                {
                    crmProxy.EnableProxyTypes();
                }

                crmProxy.Factory = this;
                proxies.Add(crmProxy.Id, crmProxy);

                return crmProxy;
            }
        }

        public OrganizationServiceProxy CreateProxyAs(Guid impersonatedSystemUser)
        {

            var proxy = CreateProxy();
            proxy.CallerId = impersonatedSystemUser;
            return proxy;

        }

        private void DownloadMetadata()
        {
            organizationServiceMetadata = ServiceConfigurationFactory.CreateManagement<IOrganizationService>(new Uri(organizationServiceUrl));
        }

        private void Authenticate(string username, string password, string domain)
        {
            var credentials = GetCredentials(organizationServiceMetadata.AuthenticationType, username, password, domain);
            authenticationCredentials = organizationServiceMetadata.Authenticate(credentials);

            if (authenticationCredentials.SecurityTokenResponse?.Token != null)
            {
                tokenExpiredTime = authenticationCredentials.SecurityTokenResponse.Token.ValidTo;
            }
        }

        private AuthenticationCredentials GetCredentials(AuthenticationProviderType endpointType, string userName,
            string password, string domain)
        {
            var authCredentials = new AuthenticationCredentials();

            switch (endpointType)
            {
                case AuthenticationProviderType.ActiveDirectory:
                    authCredentials.ClientCredentials.Windows.ClientCredential = new NetworkCredential(userName,
                        password, domain);
                    break;
                case AuthenticationProviderType.Federation:
                case AuthenticationProviderType.OnlineFederation:
                    authCredentials.ClientCredentials.UserName.UserName = userName;
                    authCredentials.ClientCredentials.UserName.Password = password;
                    break;
                case AuthenticationProviderType.None:
                    break;
                default:
                    throw new FormatException(string.Format("Authentication type '{0}' is not supported", endpointType));
            }

            return authCredentials;
        }

        public void Dispose()
        {
            for (int i = proxies.Count - 1; i >= 0; i--)
            {
                var item = proxies.ElementAt(i);
                item.Value.Dispose();
                proxies.Remove(item.Key);
            }
        }

        public IOrganizationService CreateOrganizationService(Guid? userId)
        {
            return userId.HasValue ? CreateProxyAs(userId.Value) : CreateProxy();
        }
    }

    public class CRMCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string OrganizationServiceUrl { get; set; }

        public void GenerateOrganizationServiceUrl(string organizationUrl)
        {
            OrganizationServiceUrl = Path.Combine(organizationUrl, "XRMServices/2011/Organization.svc");
        }

        public static CRMCredentials Parse(string connectionString)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                xml.LoadXml(connectionString);

                var crmCredentials = new CRMCredentials();

                var credentialValue = xml.Value;
                var nonEmptyCredentialValue = credentialValue.Replace(" ", "");
                var semicolumnSplitedCredentialValue = nonEmptyCredentialValue.Split(';');

                foreach (var splitedValue in semicolumnSplitedCredentialValue)
                {
                    var keyValue = splitedValue.Split('=');
                    var firstParameter = keyValue[0];
                    if (keyValue.Length > 1)
                    {
                        var secondParameter = keyValue[1];
                        if (firstParameter == "server")
                        {
                            crmCredentials.GenerateOrganizationServiceUrl(secondParameter);
                        }
                        else if (firstParameter == "domain")
                        {
                            crmCredentials.Domain = secondParameter;
                        }
                        else if (firstParameter == "username")
                        {
                            crmCredentials.Username = secondParameter;
                        }
                        else if (firstParameter == "password")
                        {
                            crmCredentials.Password = secondParameter;
                        }
                    }
                }


                return crmCredentials;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
