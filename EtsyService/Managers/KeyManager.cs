using System;
using System.Threading.Tasks;
using EtsyService.Managers.Interfaces;
using EtsyService.Models;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Services.AppAuthentication;
using System.Linq;

namespace EtsyService.Managers
{
    public class KeyManager : IKeyManager
    {
        private readonly KeyVaultClient keyVaultClient;
        private TelemetryClient telemetryClient = new TelemetryClient();
        private static string keyVaultUrl;
        private readonly IConfiguration configuration;

        public KeyManager(IConfiguration configuration)
        {
            this.configuration = configuration;
            keyVaultUrl = configuration["keyVaultUrl"];
            var tokenProvider = new AzureServiceTokenProvider(null, keyVaultUrl);
            keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                    tokenProvider.KeyVaultTokenCallback));
            keyVaultClient.HttpClient.BaseAddress = new Uri(keyVaultUrl);
        }

        public async Task<EtsyOAuthToken> GetEtsyTokenByCompanyId(long companyId)
        {
            try
            {
                var tokenBundle = await keyVaultClient.GetSecretAsync(keyVaultUrl, $"etsy-token-{companyId}").ConfigureAwait(false);
                var tokenSecretBundle = await keyVaultClient.GetSecretAsync(keyVaultUrl, $"etsy-token-secret-{companyId}").ConfigureAwait(false);

                return new EtsyOAuthToken
                {
                    token = tokenBundle.Value,
                    tokenSecret = tokenSecretBundle.Value
                };
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
            }
            return null;
        }

        public bool SetEtsyAuthByCompanyId(long companyId, EtsyOAuthToken token)
        {
            try
            {
                Task<bool> tokenTask = SetSecret(token.token, $"etsy-token-{companyId}");
                Task<bool> tokenSecretTask = SetSecret(token.tokenSecret, $"etsy-token-secret-{companyId}");
                Task.WaitAll(tokenTask, tokenSecretTask);
                return tokenTask.Result && tokenSecretTask.Result;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return false;
            }
        }

        private async Task<bool> SetSecret(string token, string identifier)
        {
            try
            {
                //check to see if identifier exists
                var versionList = await keyVaultClient.GetSecretVersionsAsync(keyVaultUrl, identifier);
                if (!versionList.Any())
                {
                    await keyVaultClient.UpdateSecretAsync(keyVaultUrl, identifier, token);
                }
                else //exists, update it
                {
                    await keyVaultClient.SetSecretAsync(keyVaultUrl, identifier, token);
                }

                return true;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return false;
            }
        }
    }
}
