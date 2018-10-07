using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AlexaSkill.Classes;

namespace AlexaSkill.Twitter
{
    // <summary>
    /// Simple class for sending tweets to Twitter using Single-user OAuth.
    /// https://dev.twitter.com/oauth/overview/single-user
    /// 
    /// Get your access keys by creating an app at apps.twitter.com then visiting the
    /// "Keys and Access Tokens" section for your app. They can be found under the
    /// "Your Access Token" heading.
    /// </summary>
    internal class TwitterApi
    {
        public enum Method
        {
            POST,
            GET
        }

        private const string TwitterApiBaseUrl = "https://api.twitter.com/1.1/";
        private readonly string _consumerKey, _consumerKeySecret, _accessToken, _accessTokenSecret;
        private readonly DateTime _epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private readonly HMACSHA1 _sigHasher;

        /// <summary>
        ///     Creates an object for sending tweets to Twitter using Single-user OAuth.
        ///     Get your access keys by creating an app at apps.twitter.com then visiting the
        ///     "Keys and Access Tokens" section for your app. They can be found under the
        ///     "Your Access Token" heading.
        /// </summary>
        public TwitterApi(string consumerKey, string consumerKeySecret, string accessToken,
            string accessTokenSecret)
        {
            _consumerKey = consumerKey;
            _consumerKeySecret = consumerKeySecret;
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;

            _sigHasher = new HMACSHA1(
                new ASCIIEncoding().GetBytes($"{consumerKeySecret}&{accessTokenSecret}"));
        }

        /// <summary>
        ///     Sends a tweet with the supplied text and returns the response from the Twitter API.
        /// </summary>
        public Task<string> Tweet(string text)
        {
            var data = new Dictionary<string, string>
            {
                {"status", text},
                {"trim_user", "1"}
            };

            return SendPostRequest("statuses/update.json", data);
        }

        public Task<string> SearchTweets(string search, int count)
        {
            var fullUrl = TwitterApiBaseUrl +
                          "search/tweets.json";

            var requestParameters =
                new Dictionary<string, string> {{"q", search}};

            var oAuthHeader = BuildOAuthHeader(requestParameters, fullUrl, Method.GET);

            var response = SendGetRequest(fullUrl, oAuthHeader, requestParameters);

            return response;
        }

        private Task<string> SendPostRequest(string url, Dictionary<string, string> data)
        {
            var fullUrl = TwitterApiBaseUrl + url;

            var oAuthHeader = BuildOAuthHeader(data, fullUrl, Method.POST);

            // Build the form data (exclude OAuth stuff that's already in the header).
            var formData = new FormUrlEncodedContent(data.Where(kvp => !kvp.Key.StartsWith("oauth_")));

            return SendRequest(fullUrl, oAuthHeader, formData);
        }

        private string BuildOAuthHeader(Dictionary<string, string> data, string fullUrl, Method method)
        {
            // Timestamps are in seconds since 1/1/1970.
            var timestamp = (int) (DateTime.UtcNow - _epochUtc).TotalSeconds;

            // Add all the OAuth headers we'll need to use when constructing the hash.
            data.Add("oauth_consumer_key", _consumerKey);
            data.Add("oauth_signature_method", "HMAC-SHA1");
            data.Add("oauth_timestamp", timestamp.ToString());
            data.Add("oauth_nonce", "a"); // Required, but Twitter doesn't appear to use it, so "a" will do.
            data.Add("oauth_token", _accessToken);
            data.Add("oauth_version", "1.0");

            // Generate the OAuth signature and add it to our payload.
            data.Add("oauth_signature", GenerateSignature(fullUrl, data, method));

            // Build the OAuth HTTP Header from the data.
            var oAuthHeader = GenerateOAuthHeader(data);
            return oAuthHeader;
        }

        /// <summary>
        ///     Generate an OAuth signature from OAuth header values.
        /// </summary>
        private string GenerateSignature(string url, Dictionary<string, string> data, Method method)
        {
            var sigString = string.Join(
                "&",
                data
                    .Union(data)
                    .Select(kvp =>
                        $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}")
                    .OrderBy(s => s)
            );

            var fullSigData =
                $"{(method == Method.POST ? "POST" : "GET")}&{Uri.EscapeDataString(url)}&{Uri.EscapeDataString(sigString)}";

            return Convert.ToBase64String(
                _sigHasher.ComputeHash(new ASCIIEncoding().GetBytes(fullSigData)));
        }

        /// <summary>
        ///     Generate the raw OAuth HTML header from the values (including signature).
        /// </summary>
        private string GenerateOAuthHeader(Dictionary<string, string> data)
        {
            return "OAuth " + string.Join(
                       ", ",
                       data
                           .Where(kvp => kvp.Key.StartsWith("oauth_"))
                           .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}=\"{Uri.EscapeDataString(kvp.Value)}\"")
                           .OrderBy(s => s)
                   );
        }

        /// <summary>
        ///     Send HTTP Request and return the response.
        /// </summary>
        private async Task<string> SendRequest(string fullUrl, string oAuthHeader, FormUrlEncodedContent formData)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);

                var httpResp = await http.PostAsync(fullUrl, formData);
                var respBody = await httpResp.Content.ReadAsStringAsync();

                return respBody;
            }
        }

        private async Task<string> SendGetRequest(string fullUrl, string oAuthHeader,
            Dictionary<string, string> requestParameters)
        {
            // request = (HttpWebRequest)WebRequest.Create();
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);

                var httpResp = await http.GetAsync(fullUrl + "?"
                                                           + requestParameters.ToWebString());
                var respBody = await httpResp.Content.ReadAsStringAsync();

                return respBody;
            }
        }
    }
}