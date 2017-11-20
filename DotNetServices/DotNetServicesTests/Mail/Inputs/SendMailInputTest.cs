using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolyHxDotNetServices.Mail.Inputs;

namespace DotNetServicesTests.Mail.Inputs
{
    [TestClass]
    public class SendMailInputTest
    {
        [TestMethod]
        public void HtmlOnlyMailInput()
        {
            var input = new SendMailInput
            {
                From = "PolyHx <info@polyhx.io>",
                To = new []{ "sponsor@polyhx.io" },
                Subject = "Sponsorship PolyHx",
                Html = "<h3>I want to give you some money</h3>"
            };

            var content = input.ToString();
            Assert.AreEqual("{\"from\":\"PolyHx <info@polyhx.io>\"," +
                            "\"to\":[\"sponsor@polyhx.io\"]," +
                            "\"subject\":\"Sponsorship PolyHx\"," +
                            "\"text\":null," +
                            "\"html\":\"<h3>I want to give you some money</h3>\"," +
                            "\"template\":null," +
                            "\"variables\":null}", content);
        }

        [TestMethod]
        public void TemplateVariablesInput()
        {
            var input = new SendMailInput
            {
                From = "PolyHx <info@polyhx.io>",
                To = new []{ "sponsor@polyhx.io" },
                Subject = "Sponsorship PolyHx",
                Template = "test",
                Variables = new Dictionary<string, string>
                {
                    {"name", "PolyHx"}
                }
            };

            var content = input.ToString();
            Assert.AreEqual("{\"from\":\"PolyHx <info@polyhx.io>\"," +
                            "\"to\":[\"sponsor@polyhx.io\"]," +
                            "\"subject\":\"Sponsorship PolyHx\"," +
                            "\"text\":null," +
                            "\"html\":null," +
                            "\"template\":\"test\"," +
                            "\"variables\":{\"name\":\"PolyHx\"}}", content);
        }
    }
}