namespace Website.Test
{
    using Microsoft.Owin.Testing;
    using NUnit.Framework;
    using System;
    using System.Net;
    using System.Net.Http;
    using Website;

    [TestFixture]
    public class WebsiteInfoControllerGetTest
    {
        TestServer          _apiServer;
        HttpClient          _client;
        HttpResponseMessage _response;

        public WebsiteInfoControllerGetTest()
        {
            _apiServer = TestServer.Create(new Startup().Configuration);
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
            Assert.AreEqual("Api's Info: From Web.Debug.csx AuthenticationEnabled: False", result);
#else
            Assert.AreEqual("Api's Info: From Web.Release.csx AuthenticationEnabled: True", result);
#endif
        }
    }
}
