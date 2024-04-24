using SSPRExternalApiExample.Api.Dtos;
using System.DirectoryServices;

namespace SSPRExternalApiExample.Api.Helpers
{
    /// <summary>
    /// Ornek olarak Active Directory kullanilmistir. Bu yardimci sinif kullanilmak zorunda degildir.
    /// </summary>
    public class ADHelper : Singleton<ADHelper>
    {
        public async Task<(bool isOk, string samAccountName)> GetSamAccountName(string sicilNo, DomainDto domainInfo)
        {
            try
            {
                var authenticationTypes = domainInfo.TLS ? AuthenticationTypes.SecureSocketsLayer | AuthenticationTypes.Secure : AuthenticationTypes.Secure;

                DirectoryEntry domainEntry;


                domainEntry = new DirectoryEntry($"LDAP://{domainInfo.FQDN}:{domainInfo.Port}",
                                                    domainInfo.UserName == null ? null : domainInfo.UserName.ToString(),
                                                    domainInfo.UserName == null ? null : domainInfo.Password.ToString(),
                                                    authenticationTypes);

                using var directorySearcher = new DirectorySearcher(domainEntry)
                {
                    SearchScope = SearchScope.Subtree,
                    Filter = $"(&(sicilNo={sicilNo}))"
                };

                var cObject = directorySearcher.FindOne();

                if (cObject == null)
                {
                    return await Task.FromResult<(bool, string)>((false, "User not found."));
                }

                string samAccountName = (cObject.Properties.Contains("sAMAccountName")) ? cObject.Properties["sAMAccountName"][0].ToString() : null;
                return await Task.FromResult<(bool, string)>((true, samAccountName));
            }
            catch
            {
                return await Task.FromResult<(bool, string)>((true, "sam account name"));
            }
        }
    }
}
