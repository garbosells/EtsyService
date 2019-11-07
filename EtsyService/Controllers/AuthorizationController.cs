using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EtsyService.Models;
using Microsoft.ApplicationInsights;
using EbayService.Controllers;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using OAuth;
using System.Net;
using System.IO;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace EtsyService.Controllers
{
    [ApiController]
    public class AuthorizationController : Controller
    {
        TelemetryClient telemetryClient = new TelemetryClient();
        private static string OAUTH_CONSUMER_KEY = "fss7iai057pzpsmotkdbu1do";
        private static string OAUTH_CONSUMER_SECRET = "w7dh83j6xq";
        private IConfiguration configuration;

        public AuthorizationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("api/Authorization/GenerateUserAuthorizationUrl")]
        public async Task<GenerateUserAuthorizationUrlResponse> GenerateUserAuthorizationUrlAsync()
        {
            try
            {
                string url = "https://openapi.etsy.com/v2/oauth/request_token";

                var auth = OAuthRequest.ForRequestToken(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, "http://etsyservice-test.azurewebsites.net/api/Authorization/ConfirmAuthorization");
                auth.RequestUrl = url;
                var authHeader = auth.GetAuthorizationHeader();

                var request = (HttpWebRequest)WebRequest.Create(auth.RequestUrl);
                request.Method = "GET";
                request.Headers.Add("Authorization", authHeader);

                request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
                var response = await request.GetResponseAsync();
                var responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                var responseQueryParams = HttpUtility.ParseQueryString(responseText);
                var authUrl = responseQueryParams["login_url"];

                var tokenSecret = responseQueryParams["oauth_token_secret"];
                configuration["tokenSecret"] = tokenSecret;

                // return new GenerateUserAuthorizationUrlResponse();
                return await Task.FromResult<GenerateUserAuthorizationUrlResponse>(new GenerateUserAuthorizationUrlResponse { IsSuccess = true, URL = authUrl });
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return new GenerateUserAuthorizationUrlResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("api/Authorization/ConfirmAuthorization")]
        public async Task<ActionResult> ConfirmAsync()
        {
            var result = new ViewResult();
            //WebResponse response = null;
            try
            {
                var url = "https://openapi.etsy.com/v2/oauth/access_token";

                var queryParams = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                var verifier = queryParams["oauth_verifier"];
                var token = queryParams["oauth_token"];
                var tokenSecret = configuration["tokenSecret"];

                //var auth = OAuthRequest.ForRequestToken(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET);
                var auth = OAuthRequest.ForAccessToken(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token, tokenSecret, verifier);

                auth.RequestUrl = url;
                var authHeader = auth.GetAuthorizationHeader();

                //var request = (HttpWebRequest)WebRequest.Create(auth.RequestUrl);
                //request.Method = "GET";
                //request.Headers.Add("Authorization", authHeader);

                //request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", authHeader);
                var response = await client.GetAsync(url);
                var message = await response.Content.ReadAsStringAsync();

                result.ViewName = "Confirm";
                return result;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex, new Dictionary<string, string> { { "ErrorMessage", ex.Message } });
            }
            result.ViewName = "Error";
            return result;
        }
    }
}
