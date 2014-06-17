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
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using Website;
    using System.Reflection;
    using Newtonsoft.Json.Serialization;

    [TestFixture]
    public class WebsiteInfoControllerGetTest
    {
        TestServer          _apiServer;
        HttpClient          _client;
        HttpResponseMessage _response;

        public WebsiteInfoControllerGetTest()
        {
            _apiServer = TestServer.Create<WebsiteStartup>();
            _client = _apiServer.HttpClient;
            _client.BaseAddress = new Uri("https://localhost:4445/");
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _response  = _apiServer.HttpClient.GetAsync("api/info").Result;           
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _apiServer.Dispose();
        }

        [Test, Description("Test API.InfoController's response to HTTP [GET] request IsSuccessStatusCode"), Category("Website.Test.InfoControllerGetTest")]
        public void ApiResponseIsSuccessStatusCodeTest()
        {    
            Assert.AreEqual(true, _response.IsSuccessStatusCode);
        }

        [Test, Description("Test API.InfoController's response to HTTP [GET] request is 'OK'"), Category("Website.Test.InfoControllerGetTest")]
        public void ApiResponseIsOkTest()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Test, Description("Test API.InfoController's response MediaType to HTTP [GET] request is application/json"), Category("Website.Test.InfoControllerGetTest")]
        public void ApiResponseContentTypeIsJson()
        {
            Assert.AreEqual("application/json", _response.Content.Headers.ContentType.MediaType);
        }

        [Test, Description("Test API.InfoController's response to HTTP [GET] request reflected [GET] operation"), Category("Website.Test.InfoControllerGetTest")]
        public void ApiResponseMethodIsGetTest()
        {
            Assert.AreEqual(HttpMethod.Get, _response.RequestMessage.Method);
        }

        [Test, Description("Test API.InfoController's response to HTTP [GET] request for the execution configuration "), Category("Website.Test.InfoControllerGetTest")]
        public void ApiResponseContentByConfigurationTest()
        {
            dynamic result = _response.Content.ReadAsAsync<dynamic>().Result;
#if DEBUG
            Assert.AreEqual("Website's Info: Vader", result);
#else
            Assert.AreEqual("Website's Info: Yoda", result);
#endif
        }
    }
}
