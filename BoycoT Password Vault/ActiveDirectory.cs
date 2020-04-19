using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;

namespace BoycoT_Password_Vault
{
    static class ActiveDirectory
    {
        internal static List<string> GetDomainNames()
        {
            List<string> lst = new List<string>();
            // Querying the current Forest for the domains within.
            foreach (Domain d in Forest.GetCurrentForest().Domains)
                if (d.Name != "ups.com")
                    lst.Add(d.Name.Replace(".ups.com", "").ToUpper());
            return lst;
        }

        internal static bool validateAD(string domain, string user, string pass)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                return pc.ValidateCredentials(user, pass);
            }
        }

    }
}
