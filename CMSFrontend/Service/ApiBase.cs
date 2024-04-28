using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using RestSharp;


namespace CMSFrontend.Service
{
    public class ApiBase
    {
       // private static ObjectCache _tokenCache = MemoryCache.Default;
        public ResponseModel<T> CallApi<T>(string apiUrl, string value, string baseUrl, Method method = Method.GET, string paramName = "application/json", ParameterType parameterType = ParameterType.RequestBody, string contentType = "application/json", string domainId = "", string orgId = "", string userName = "")
        {

            var restClient = new RestClient(baseUrl);
            var restRequest = new RestRequest(apiUrl, method);

            if (!string.IsNullOrEmpty(value))
            {
                var param = new RestSharp.Parameter(paramName, value, contentType, parameterType);
                restRequest.AddParameter(param);
            }
            if (!string.IsNullOrEmpty(orgId))
                restRequest.AddHeader("orgId", orgId);
            if (!string.IsNullOrEmpty(domainId))
                restRequest.AddHeader("DomainId", domainId);
            if (!string.IsNullOrEmpty(userName))
                restRequest.AddHeader("Email", userName);

            //restRequest.AddHeader("token", Token().Token);
            //restRequest.AddHeader("DeviceId", ReadCookie(Constants.COOKIE_DEVICEID));
            //restRequest.AddHeader("SessionId", ReadCookie(Constants.COOKIE_SESSIONID));
            //AddDefaultHeader(ref restRequest);

          //  var authService = DependencyResolver.Current.GetService<IAuthService>();

            //var token = authService.GetAccessToken(ConfigKeys.AuthorizationServerBaseAddress, ConfigKeys.AuthClientId, ConfigKeys.AuthClientSecret);
            //restRequest.AddParameter("Authorization", "Bearer " + token.Token, ParameterType.HttpHeader);

            var restResponse = restClient.Execute(restRequest);
            try
            {
                var result = JsonConvert.DeserializeObject<ResponseModel<T>>(restResponse.Content);
                return result;
            }
            catch (Exception)
            {
                return new ResponseModel<T> { };
            }

        }

    //    public async Task<ResponseModel<T>> CallApiAsync<T>(string apiUrl, string value, string baseUrl, Method method = Method.GET, string paramName = "application/json", ParameterType parameterType = ParameterType.RequestBody, string contentType = "application/json")
    //    {

    //        var restClient = new RestClient(baseUrl);
    //        var restRequest = new RestRequest(apiUrl, method);

    //        if (!string.IsNullOrEmpty(value))
    //        {
    //            var param = new Parameter(paramName, value, contentType, parameterType);
    //            restRequest.AddParameter(param);
    //        }
    //        //restRequest.AddHeader("token", Token().Token);
    //        //restRequest.AddHeader("DeviceId", ReadCookie(Constants.COOKIE_DEVICEID));
    //        restRequest.AddHeader("SessionId", "foundationService");
    //        //AddDefaultHeader(ref restRequest);
    //        var cancellationTokenSource = new CancellationTokenSource();

    //        var tcs = new TaskCompletionSource<ResponseModel<T>>();

    //        restClient.ExecuteAsync(restRequest, response =>
    //        {
    //            try
    //            {
    //                tcs.SetResult(JsonConvert.DeserializeObject<ResponseModel<T>>(response.Content));
    //                //return tcs.Task;
    //            }
    //            catch (Exception)
    //            {
    //                tcs.SetResult((ResponseModel<T>)Activator.CreateInstance(typeof(ResponseModel<T>)));
    //            }
    //        });
    //        return tcs.Task.Result;

    //    }
    //    public ResponseModel<T> CallCoreApi<T>(string apiUrl, string value, string baseUrl, Method method = Method.GET, string paramName = "application/json", ParameterType parameterType = ParameterType.RequestBody, string contentType = "application/json", string domainId = "", string orgId = "", string userName = "")
    //    {

    //        var restClient = new RestClient(baseUrl);
    //        var restRequest = new RestRequest(apiUrl, method);

    //        if (!string.IsNullOrEmpty(value))
    //        {
    //            var param = new Parameter(paramName, value, contentType, parameterType);
    //            restRequest.AddParameter(param);
    //        }
    //        if (!string.IsNullOrEmpty(orgId))
    //            restRequest.AddHeader("orgId", orgId);
    //        if (!string.IsNullOrEmpty(domainId))
    //            restRequest.AddHeader("DomainId", domainId);
    //        if (!string.IsNullOrEmpty(userName))
    //            restRequest.AddHeader("Email", userName);

    //        var _clientId = ConfigKeys.AuthClientId;
    //        var _clientSecret = ConfigKeys.AuthClientSecret;
    //        var _authUrl = ConfigKeys.SSOUrl;
    //        #region Core Token generation
    //        AccessToken token = null;
    //        string OAuthCacheKey = "oauthcore.{0}";
    //        bool isTokenValid = false;
    //        if (_tokenCache.Get(String.Format(OAuthCacheKey, _clientId)) != null)
    //        {
    //            token = (AccessToken)_tokenCache.Get(String.Format(OAuthCacheKey, _clientId));
    //            isTokenValid = (token.Expires > DateTime.UtcNow);
    //        }
    //        if (!isTokenValid)
    //        {
    //            var authClient = new RestClient(_authUrl);
    //            var autRequest = new RestRequest(Method.POST);
    //            autRequest.AddParameter("grantType", "client_credentials", ParameterType.QueryString);
    //            autRequest.AddParameter("clientId", _clientId, ParameterType.QueryString);
    //            autRequest.AddParameter("clientSecret", _clientSecret, ParameterType.QueryString);
    //            //make the API request and get the response
    //            if (_authUrl.ToLower().StartsWith("https"))
    //            {
    //                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    //            }
    //            IRestResponse response = authClient.Execute(autRequest);

    //            ////return an AccessToken
    //            token = JsonConvert.DeserializeObject<AccessToken>(response.Content);
    //            if (token == null)
    //            {
    //                var error = JsonConvert.SerializeObject(response);
    //                throw new Exception("Error getting the token : " + response.ErrorMessage + response.ErrorException.InnerException.Message);
    //            }
    //            if (!string.IsNullOrEmpty(token.Token))
    //            {
    //                token.Expires = DateTime.UtcNow.AddSeconds(token.ExpiresIn);
    //                var cahePolicy = new CacheItemPolicy() { AbsoluteExpiration = token.Expires };
    //                _tokenCache.Set(String.Format(OAuthCacheKey, _clientId), token, cahePolicy);
    //            }

    //        }
    //        #endregion
    //        restRequest.AddParameter("Authorization", token.Token, ParameterType.HttpHeader);

    //        var restResponse = restClient.Execute(restRequest);
    //        try
    //        {
    //            var result = JsonConvert.DeserializeObject<ResponseModel<T>>(restResponse.Content);
    //            return result;
    //        }
    //        catch (Exception)
    //        {
    //            return new ResponseModel<T> { };
    //        }

    //    }
    }


    [Serializable]
    public class ResponseModel<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Status { get; set; }

        public string Error { get; set; }

        public string ErrorId { get; set; }

        public string Message { get; set; }

        public string MessageCode { get; set; }

        public T Result { get; set; }
    }
}