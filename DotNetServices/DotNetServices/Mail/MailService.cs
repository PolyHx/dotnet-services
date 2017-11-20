using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using PolyHxDotNetServices.Sts;
using PolyHxDotNetServices.Mail.Inputs;

namespace PolyHxDotNetServices.Mail
{
    public class MailService: IMailService
    {
        public string ApiUrl { get; }
        private readonly HttpClient _httpClient;
        private readonly IStsService _stsService;

        public MailService(IStsService stsService, HttpClient client = null)
        {
            _httpClient = client ?? new HttpClient();
            _stsService = stsService;
            ApiUrl = Environment.GetEnvironmentVariable("MAIL_API_URL");
        }

        public async Task<bool> SendEmail(SendMailInput input)
        {
            var dictionary = input.ToDictionnary();
            var content = new FormUrlEncodedContent(dictionary);

            HttpResponseMessage result;
            try
            {
                var token = await _stsService.GetAccessToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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