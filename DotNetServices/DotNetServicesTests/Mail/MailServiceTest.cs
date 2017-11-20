using System.Net.Http;
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

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            System.Environment.SetEnvironmentVariable("MAIL_API_URL", "http://localhost/mail");
        }
        
        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttp = new MockHttpMessageHandler();           
            _mailService = new MailService(new HttpClient(_mockHttp));
            _mockHttp.When(_mailService.ApiUrl + "/*")
                .Respond("application/json", "{}");
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