using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PolyHxDotNetServices.Sts.Outputs;
using Newtonsoft.Json;

namespace PolyHxDotNetServices.Sts
{
    public class StsService : IStsService
    {
        public string ApiUrl { get; }
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _clientScopes;
        private string _accessToken;
        private readonly HttpClient _httpClient;

        private async Task<string> RenewToken()
        {
            var dictionnary = new Dictionary<string, string>
            {
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "scope", _clientScopes },
                { "grant_type", "client_credentials" }
            };
            var content = new FormUrlEncodedContent(dictionnary);

            var res = await _httpClient.PostAsync($"{ApiUrl}/connect/token", content);

            if (res.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Sts returns StatusCode {res.StatusCode}");

            var body = await res.Content.ReadAsStringAsync();
            var output = JsonConvert.DeserializeObject<GetAccessTokenOutput>(body);

            return output.AccessToken;
        }

        private static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+');
            output = output.Replace('_', '/');

            switch (output.Length % 4)
            {
                case 0:
                    break;
                case 2:
                    output += "==";
                    break;
                case 3:
                    output += "=";
                    break;
                default:
                    throw new Exception("Illegal base64url string!");
            }

            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }

        private bool ValidateToken()
        {
            if (_accessToken == null)
                return false;

            // Extract payload from jwt token
            var decodedPayload = Encoding.UTF8.GetString(Base64UrlDecode(_accessToken.Split('.')[1]));
            var payload = JsonConvert.DeserializeObject<AccessToken>(decodedPayload);
            var now = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            return now < payload.Exp;
        }

        public StsService(HttpClient client = null, string accessToken = null)
        {
            _httpClient = client ?? new HttpClient();
            _accessToken = accessToken;

            ApiUrl = Environment.GetEnvironmentVariable("STS_API_URL");
            _clientId = Environment.GetEnvironmentVariable("STS_CLIENT_ID");
            _clientSecret = Environment.GetEnvironmentVariable("STS_CLIENT_SECRET");
            _clientScopes = Environment.GetEnvironmentVariable("STS_CLIENT_SCOPE");
        }

        public async Task<string> GetAccessToken()
        {
            if (!ValidateToken())
                _accessToken = await RenewToken();

            return _accessToken;
        }
    }
}