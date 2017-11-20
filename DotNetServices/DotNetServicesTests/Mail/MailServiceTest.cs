using System.Net.Http;
using PolyHxDotNetServices.Sts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using PolyHxDotNetServices.Mail;
using PolyHxDotNetServices.Mail.Inputs;

namespace DotNetServicesTests.Mail
{
    [TestClass]
    public class MailServiceTest
    {
        private MockHttpMessageHandler _mockHttp;
        private MailService _mailService;
        private StsService _stsService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            System.Environment.SetEnvironmentVariable("MAIL_API_URL", "http://localhost/mail");
            System.Environment.SetEnvironmentVariable("STS_API_URL", "http://localhost/identity");
        }
        
        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttp = new MockHttpMessageHandler();
            _stsService = new StsService(new HttpClient(_mockHttp));
            _mailService = new MailService(new StsService(new HttpClient(_mockHttp)), new HttpClient(_mockHttp));
            _mockHttp.When(_mailService.ApiUrl + "/mail")
                .Respond("application/json", "{}");
            _mockHttp.When($"{_stsService.ApiUrl}/connect/token")
                .Respond("application/json", "{ \"access_token\": \"token\" }");
        }

        [TestMethod]
        public void SendMailTest()
        {
            var input = new SendMailInput
            {
                From = "PolyHx <info@polyhx.io>",
                To = new []{ "sponsor@polyhx.io" },
                Subject = "Sponsorship PolyHx",
                Html = "<h3>I want to give you some money</h3>"
            };

            var res = _mailService.SendEmail(input).Result;
            Assert.IsTrue(res);
        }
    }
}