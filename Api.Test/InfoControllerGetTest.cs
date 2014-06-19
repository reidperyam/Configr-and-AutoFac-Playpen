namespace Api.Test
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
    using Api;
    using System.Reflection;

    [TestFixture]
    public class InfoControllerGetTest
    {
        TestServer          _apiServer;
        HttpClient          _client;
        HttpResponseMessage _response;

        public InfoControllerGetTest()
        {
            _apiServer = TestServer.Create(new ApiStartup().Configuration);
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

        [Test, Description("Test API.InfoController's response to HTTP [GET] request IsSuccessStatusCode"), Category("Api.Test.InfoControllerGetTest")]
        public void ResponseIsSuccessStatusCodeTest()
        {    
            Assert.AreEqual(true, _response.IsSuccessStatusCode);
        }

        [Test, Description("Test API.InfoController's response to HTTP [GET] request is 'OK'"), Category("Api.Test.InfoControllerGetTest")]
        public void ResponseIsOkTest()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Test, Description("Test API.InfoController's response MediaType to HTTP [GET] request is application/json"), Category("Api.Test.InfoControllerGetTest")]
        public void ResponseContentTypeIsJson()
        {
            Assert.AreEqual("application/json", _response.Content.Headers.ContentType.MediaType);
        }

        [Test, Description("Test API.InfoController's response to HTTP [GET] request reflected [GET] operation"), Category("Api.Test.InfoControllerGetTest")]
        public void ResponseMethodIsGetTest()
        {
            Assert.AreEqual(HttpMethod.Get, _response.RequestMessage.Method);
        }

        [Test, Description("Test API.InfoController's response to HTTP [GET] request for the execution configuration "), Category("Api.Test.InfoControllerGetTest")]
        public void ResponseContentByConfigurationTest()
        {
            dynamic result = _response.Content.ReadAsAsync<dynamic>().Result;
#if DEBUG
            Assert.AreEqual("Api's Info: From Api.Debug.csx AuthenticationEnabled: False", result);
#else
            Assert.AreEqual("Api's Info: From Api.Release.csx AuthenticationEnabled: True", result);
#endif
        }
    }
}
