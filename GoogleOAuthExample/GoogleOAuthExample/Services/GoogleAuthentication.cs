using Google.Apis.AndroidManagement.v1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Web;
using IdentityModel.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GoogleOAuthExample.Services
{
    public class GoogleAuthentication
    {
        private string currentCSRFToken;

        public async Task<bool> LogIn()
        {
            string redirectUri = "com.companyname.googleoauthexample:/oauth2redirect";
            var client_id = "xxxxxxxxxxxxxxxxxxxxxxxxx.apps.googleusercontent.com";
            var authUrl = CreateAuthorizationRequest(client_id, redirectUri, AndroidManagementService.Scope.Androidmanagement);

            var authResult = await WebAuthenticator.AuthenticateAsync(new Uri(authUrl), new Uri(redirectUri));       

            var code = authResult.Properties["code"];

            var client = new RestClient(new Uri("https://oauth2.googleapis.com"));
            var request = new RestRequest("token", Method.Post);
            request.AddParameter("code", code);
            request.AddParameter("client_id", client_id);
            request.AddParameter("redirect_uri", redirectUri);
            request.AddParameter("grant_type", "authorization_code");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            var response = await client.ExecuteAsync<MyAuthResult>(request);

            return true;
        }

        public string CreateAuthorizationRequest(string clientId, string redirectURI, string scope)
        {
            // Create URI to authorization endpoint
            var authorizeRequest = new RequestUrl(GoogleAuthConsts.AuthorizationUrl);
            currentCSRFToken = Guid.NewGuid().ToString("N");

            var dic = new Dictionary<string, string>
            {
                {"client_id", clientId},
                {"response_type", "code"},
                {"scope", scope},
                {"redirect_uri", redirectURI},
                {"prompt", "consent"},

                {"state", currentCSRFToken}  , // Add CSRF token to protect against cross-site request forgery attacks.
                {"token_uri", "https://accounts.google.com/o/oauth2/token" }
            };

            var authorizeUri = authorizeRequest.Create(Parameters.FromObject(dic));
            return authorizeUri;
        }
    }
}