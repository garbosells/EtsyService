using System;
using System.Threading.Tasks;
using EtsyService.Models;

namespace EtsyService.Managers
{
    public interface IAuthorizationManager
    {
        bool SetEtsyAuth(string accessToken, string accessTokenSecret, long companyId);
        Task<EtsyOAuthToken> GetTokenByCompanyId(long companyId);
    }
}
