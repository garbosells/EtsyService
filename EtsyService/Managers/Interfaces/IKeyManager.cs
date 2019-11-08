using System;
using System.Threading.Tasks;
using EtsyService.Models;

namespace EtsyService.Managers.Interfaces
{
    public interface IKeyManager
    {
        bool SetEtsyAuthByCompanyId(long companyId, EtsyOAuthToken token);
        Task<EtsyOAuthToken> GetEtsyTokenByCompanyId(long company);
    }
}
