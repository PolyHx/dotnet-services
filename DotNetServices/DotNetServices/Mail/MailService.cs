using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using PolyHxDotNetServices.Mail.Inputs;

namespace PolyHxDotNetServices.Mail
{
    public class MailService
    {
        public string ApiUrl { get; }
        private readonly HttpClient _httpClient;

        public MailService()
        {
            _httpClient = new HttpClient();
            ApiUrl = Environment.GetEnvironmentVariable("MAIL_API_URL");
        }
        
        public MailService(HttpClient client)
        {
            _httpClient = client;
            ApiUrl = Environment.GetEnvironmentVariable("MAIL_API_URL");
        }

        public async Task<bool> SendEmail(SendMailInput input)
        {
            var dictionary = input.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => 
                    prop.GetValue(input, null) == null ? "" : prop.GetValue(input, null).ToString());
            var content = new FormUrlEncodedContent(dictionary);

            HttpResponseMessage result;
            try
            {
                result = await _httpClient.PostAsync(ApiUrl + "/mail", content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result.StatusCode == HttpStatusCode.OK;
        }
    }
}