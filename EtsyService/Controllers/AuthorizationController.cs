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

namespace EtsyService.Controllers
{
  [ApiController]
  public class AuthorizationController : Controller
  {
    TelemetryClient telemetryClient = new TelemetryClient();

    [HttpGet]
    [Route("api/Authorization/GenerateUserAuthorizationUrl")]
    public async Task<GenerateUserAuthorizationUrlResponse> GenerateUserAuthorizationUrlAsync()
    {
      try
      {
        string url = "https://openapi.etsy.com/v2/oauth/request_token";
        string OAUTH_CONSUMER_KEY = "fss7iai057pzpsmotkdbu1do";
        string OAUTH_CONSUMER_SECRET = "w7dh83j6xq";

        var auth = OAuthRequest.ForRequestToken(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET,"oob");
        auth.RequestUrl = url;
        var authHeader = auth.GetAuthorizationHeader();

        var request = (HttpWebRequest)WebRequest.Create(auth.RequestUrl);
        request.Method = "GET";
        request.Headers.Add("Authorization", authHeader);
        
        request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
        var response = await request.GetResponseAsync();
        var message = new StreamReader(response.GetResponseStream()).ReadToEnd();

        return new GenerateUserAuthorizationUrlResponse();
       // return await Task.FromResult<GenerateUserAuthorizationUrlResponse>(new GenerateUserAuthorizationUrlResponse { IsSuccess = true, URL = url });
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

    [HttpPost]
    [Route("api/Authorization/Test")]
    public void Test()
    {
      var x = Request;
    } 
  }
}
