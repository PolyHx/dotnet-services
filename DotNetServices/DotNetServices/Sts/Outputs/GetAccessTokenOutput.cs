using Newtonsoft.Json;

namespace PolyHxDotNetServices.Sts.Outputs
{
    public class GetAccessTokenOutput
    {
        [JsonProperty("access_token")]
        public string AccessToken;
        
        [JsonProperty("expires_in")]
        public int ExpiresIn;
        
        [JsonProperty("token_type")]
        public string TokenType;
        
        [JsonProperty("refresh_token")]
        public string RefreshToken;
    }
}