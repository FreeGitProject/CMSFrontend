using System;
using System.Net;
using System.Runtime.Caching;
using Newtonsoft.Json;
using RestSharp;
namespace CMSFrontend.Service
{
    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("token_type")]
        public string Type { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("issued")]
        public DateTime Issued { get; set; }

        [JsonProperty("expires")]
        public DateTime Expires { get; set; }
    }
    public static class TokenManager
    {
        private static readonly ObjectCache _tokenCache = MemoryCache.Default;

        public static AccessToken GetAccessToken(string authUrl, string username, string password)
        {
            AccessToken accessToken = null;
            string cacheKey = "oauthToken";

            // Check if access token exists in cache and is not expired
            if (_tokenCache.Get(cacheKey) is AccessToken cachedToken && cachedToken.Expires > DateTime.UtcNow)
            {
                accessToken = cachedToken;
            }

            if (accessToken == null)
            {
                // Create request body as JSON
                var requestBody = new
                {
                    Username = username,
                    Password = password
                };

                // Serialize request body to JSON string
                var requestBodyJson = JsonConvert.SerializeObject(requestBody);

                // Make REST request to retrieve access token
                var restClient = new RestClient(authUrl);
                var restRequest = new RestRequest("/api/generate-token", Method.POST);

                // Set request content type and body
                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddParameter("application/json", requestBodyJson, ParameterType.RequestBody);

                // Set TLS 1.2 for HTTPS requests
                if (authUrl.ToLower().StartsWith("https"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }

                IRestResponse restResponse = restClient.Execute(restRequest);

                // Deserialize response into AccessToken object
                accessToken = JsonConvert.DeserializeObject<AccessToken>(restResponse.Content);

                if (accessToken == null || string.IsNullOrEmpty(accessToken.Token))
                {
                    throw new Exception("Error getting the access token: " + restResponse.ErrorMessage);
                }

                // Calculate expiration time and cache the access token
                accessToken.Expires = DateTime.UtcNow.AddSeconds(accessToken.ExpiresIn);
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = accessToken.Expires
                };
                _tokenCache.Set(cacheKey, accessToken, policy);
            }

            return accessToken;
        }
    }
}