using System.Net.Http;
using PolyHxDotNetServices.Sts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace DotNetServicesTests.Sts
{
    [TestClass]
    public class StsServiceTest
    {
        private MockHttpMessageHandler _mockHttp;
        private StsService _stsService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            System.Environment.SetEnvironmentVariable("STS_API_URL", "http://localhost/identity");
        }
        
        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttp = new MockHttpMessageHandler();
            _stsService = new StsService(new HttpClient(_mockHttp));
            _mockHttp.When($"{_stsService.ApiUrl}/connect/token")
                .Respond("application/json", "{ \"access_token\": \"token\" }");
        }

        [TestMethod]
        public void GetAccessToken()
        {
            var token = _stsService.GetAccessToken().Result;
            Assert.AreEqual("token", token);
        }
    }
}