using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace ODataClientNopCommerce
{
    public class Token
    {
        private string apitoken { get; set; }

        public string ApiToken
        {
            get
            {
                return apitoken;
            }
        }

        public Token(string url, string username, string password)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var credentials = new GenerateTokenModel();
            credentials.UserName = username;
            credentials.Password = Base64Encode(password);

            var serializedJson = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(serializedJson.ToString(), Encoding.UTF8, "application/json");
            var result = client.PostAsync("/api/token", httpContent).Result;
            apitoken = result.Content.ReadAsStringAsync().Result;
        }
        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
    public class GenerateTokenModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
