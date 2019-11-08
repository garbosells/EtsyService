using System;
using System.Threading.Tasks;
using EtsyService.Managers.Interfaces;
using EtsyService.Models;

namespace EtsyService.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IKeyManager keyManager;
        public AuthorizationManager(IKeyManager keyManager)
        {
            this.keyManager = keyManager;
        }

        public Task<EtsyOAuthToken> GetTokenByCompanyId(long companyId)
        {
            return keyManager.GetEtsyTokenByCompanyId(companyId);
        }

        public bool SetEtsyAuth(string accessToken, string accessTokenSecret, long companyId)
        {
            return keyManager.SetEtsyAuthByCompanyId(companyId, new EtsyOAuthToken { token = accessToken, tokenSecret = accessTokenSecret });
        }
    }
}
