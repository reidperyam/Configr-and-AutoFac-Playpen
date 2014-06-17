namespace Website.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Microsoft.Owin.Testing;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Website;
    using System.Reflection;

    [TestFixture]
    public class WebsiteInfoModuleGetTest
    {
        TestServer          _apiServer;
        HttpClient          _client;
        HttpResponseMessage _response;

        public WebsiteInfoModuleGetTest()
        {
            _apiServer = TestServer.Create<WebsiteStartup>();
            _client = _apiServer.HttpClient;
            _client.BaseAddress = new Uri("https://localhost:4445/");
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _response  = _apiServer.HttpClient.GetAsync("info").Result;           
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _apiServer.Dispose();
        }

        [Test, Description("Test Core.InfoModule's response to HTTP [GET] request IsSuccessStatusCode"), Category("Website.Test.InfoControllerGetTest")]
        public void CoreResponseIsSuccessStatusCodeTest()
        {    
            Assert.AreEqual(true, _response.IsSuccessStatusCode);
        }

        [Test, Description("Test Core.InfoModule's response to HTTP [GET] request is 'OK'"), Category("Website.Test.InfoControllerGetTest")]
        public void CoreResponseIsOkTest()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Test, Description("Test that Core.InfoModule responds to an HTTP [GET] request with application+text"), Category("Website.Test.InfoModuleGetTest")]
        public void CoreResponseContentTypeIsTextTest()
        {
            Assert.AreEqual("text/html", _response.Content.Headers.ContentType.MediaType);
        }

        [Test, Description("Test Core.InfoModule's response to HTTP [GET] request reflected [GET] operation"), Category("Website.Test.InfoControllerGetTest")]
        public void CoreResponseMethodIsGetTest()
        {
            Assert.AreEqual(HttpMethod.Get, _response.RequestMessage.Method);
        }

        [Test, Description("Test Core.InfoModule's response to HTTP [GET] request for the execution configuration "), Category("Website.Test.InfoControllerGetTest")]
        public void CoreResponseContentByConfigurationTest()
        {
            string result = _response.Content.ReadAsStringAsync().Result;
#if DEBUG
            Assert.AreEqual("Website's Info: Vader", result);
#else
            Assert.AreEqual("Website's Info: Yoda", result);
#endif
        }
    }
}
