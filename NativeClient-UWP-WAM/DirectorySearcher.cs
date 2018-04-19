using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NativeClient_UWP_WAM
{
    /// <summary>
    /// 
    /// </summary>
    public static class DirectorySearcher
    {
        private const string graphResourceUri = "https://graph.microsoft.com";
        public static string graphApiVersion = "v1.0";

        public static async Task<List<UserSearchResult>> SearchByAlias(string alias, string accessToken, string tenantId)
        {
            JObject jResult = null;
            List<UserSearchResult> results = new List<UserSearchResult>();

            string graphRequest = String.Format(CultureInfo.InvariantCulture, "{0}/{1}/users?$filter=startswith(mailNickName,'{2}')", graphResourceUri, graphApiVersion, alias);
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, graphRequest);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            jResult = JObject.Parse(content);

            if (jResult["odata.error"] != null)
            {
                throw new Exception((string)jResult["odata.error"]["message"]["value"]);
            }
            if (jResult["value"] == null)
            {
                throw new Exception("Unknown Error.");
            }
            foreach (JObject result in jResult["value"])
            {
                results.Add(new UserSearchResult
                {
                    displayName = (string)result["displayName"],
                    givenName = (string)result["givenName"],
                    surname = (string)result["surname"],
                    userPrincipalName = (string)result["userPrincipalName"],
                    telephoneNumber = (string)result["telephoneNumber"] == null ? "Not Listed." : (string)result["telephoneNumber"]
                });
            }

            return results;
        }
    }

    public class UserSearchResult
    {
        public string displayName { get; set; }
        public string userPrincipalName { get; set; }
        public string givenName { get; set; }
        public string surname { get; set; }
        public string telephoneNumber { get; set; }
        public string error { get; set; }
    }
}