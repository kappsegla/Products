using System;
using System.Net;
using System.Text;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using TwitterSample.OAuth;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace ProductsMVC.Services
{
    public class TwitterService
    {

        #region
        //Personal key and secret, identifies my user
        private string oauthtoken = "3656159795-J57rFwfpMtopt5AUy1PshCOJCZnu5CMwqBJum6G";
        private string oauthtokensecret = "zmvznIyi58vwaC3bYxXjQSrCaqRI11yO72mCSUFp1surN";
        //Application key and secret. Read only functionality
        private string oauthconsumerkey = "5h85TSNvNTyO6QL8n3nx1NDEL";
        private string oauthconsumersecret = "JxhXIkiEPLEaXaIEohJRJ7rZGCwk1VVqWjedIHdeAnyJSVnVyr";
        #endregion

        public string Search(string searchstring)
        {
            //Application only request
            string s = HttpUtility.UrlEncode(oauthconsumerkey)
                        + ":"
                        + HttpUtility.UrlEncode(oauthconsumersecret);

            string encoded_credentials = Base64Encode(s);

            var client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage();
            message.RequestUri = new Uri("https://api.twitter.com/oauth2/token");
            message.Method = HttpMethod.Post;
            message.Headers.Authorization = new AuthenticationHeaderValue("Basic", encoded_credentials);
            message.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = client.SendAsync(message).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }

            Bearer_Token bearer_token = JsonConvert.DeserializeObject<Bearer_Token>(response.Content.ReadAsStringAsync().Result);
            if (!bearer_token.token_type.Equals("bearer"))
            {
                return "Bearer token not found";
            }

            //Sucess. Now try to load some data.
            //return "Success: " + bearer_token.access_token;
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearer_token.access_token);
            request.RequestUri = new Uri("https://api.twitter.com/1.1/search/tweets.json?q=@noradio");
            var response2 = client.SendAsync(request).Result;
            return response2.Content.ReadAsStringAsync().Result;
        }

        public OAuth_Token OAuth_Request_Token()
        {
            //https://dev.twitter.com/web/sign-in/implementing
            //https://dev.twitter.com/oauth/overview/authorizing-requests

            //Step:1 Obtaining a request token     
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.twitter.com/oauth/request_token");
            request.Method = HttpMethod.Post;

            OAuthBase _oAuthBase = new OAuthBase();

            string callback_url = "http://127.0.0.1:53284/sv/Products/Callback";

            string normalizedUri;
            string normalizedParameters;
            string authHeader;

            string signature = _oAuthBase.GenerateSignature(
                request.RequestUri,
                callback_url,
                oauthconsumerkey,
                oauthconsumersecret,
                null,
                null,
                request.Method.Method,
                _oAuthBase.GenerateTimeStamp(),
                _oAuthBase.GenerateNonce(),
                out normalizedUri,
                out normalizedParameters,
                out authHeader);

            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);

            var response = client.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
                return null;

            string message = response.Content.ReadAsStringAsync().Result;
            NameValueCollection qs = HttpUtility.ParseQueryString(message);
            OAuth_Token oauth_token = new OAuth_Token();

            if (qs["oauth_token"] != null)
                     oauth_token.oauth_token = qs["oauth_token"];
            if (qs["oauth_token_secret"] != null)
                oauth_token.oauth_token_secret = qs["oauth_token_secret"];
            if (qs["oauth_callback_confirmed"] != null && qs["oauth_callback_confirmed"].Equals("true"))
                oauth_token.oauth_callback_confirmed = true;

            return oauth_token;
        }

        public OAuth_Token OAuth_Convert_Request_To_Access_Token(OAuth_Token token)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.twitter.com/oauth/access_token");
            request.Method = HttpMethod.Post;

            OAuthBase _oAuthBase = new OAuthBase();

            string normalizedUri;
            string normalizedParameters;
            string authHeader;

            string signature = _oAuthBase.GenerateSignature(
                request.RequestUri,
                null,
                oauthconsumerkey,
                oauthconsumersecret,
                token.oauth_token,
                null,
                request.Method.Method,
                _oAuthBase.GenerateTimeStamp(),
                _oAuthBase.GenerateNonce(),
                out normalizedUri,
                out normalizedParameters,
                out authHeader);

            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);
            request.Content = new StringContent("oauth_verifier="+token.oauth_token_secret, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = client.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
                return null;

            string message = response.Content.ReadAsStringAsync().Result;
            NameValueCollection qs = HttpUtility.ParseQueryString(message);
            OAuth_Token oauth_token = new OAuth_Token();
            if (qs["oauth_token"] != null)
                oauth_token.oauth_token = qs["oauth_token"];
            if (qs["oauth_token_secret"] != null)
                oauth_token.oauth_token_secret = qs["oauth_token_secret"];

            return oauth_token;
        }

        public string GetSettings(OAuth_Token token)
        {
                HttpClient client = new HttpClient();

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://api.twitter.com/1.1/account/settings.json");
                request.Method = HttpMethod.Get;

                OAuthBase _oAuthBase = new OAuthBase();

                string normalizedUri;
                string normalizedParameters;
                string authHeader;

                string signature = _oAuthBase.GenerateSignature(
                    request.RequestUri,
                    null,
                    oauthconsumerkey,
                    oauthconsumersecret,
                    token.oauth_token,
                    token.oauth_token_secret,
                    request.Method.Method,
                    _oAuthBase.GenerateTimeStamp(),
                    _oAuthBase.GenerateNonce(),
                    out normalizedUri,
                    out normalizedParameters,
                    out authHeader);

                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);
              
                var response = client.SendAsync(request).Result;
                if (!response.IsSuccessStatusCode)
                    return null;

               return response.Content.ReadAsStringAsync().Result;
            }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

    public class Bearer_Token
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }
    public class OAuth_Token
    {
        public string oauth_token;
        public string oauth_token_secret;
        public bool oauth_callback_confirmed;
    }
}

