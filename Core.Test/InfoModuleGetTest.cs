namespace Core.Test
{
    using Core;
    using Nancy;
    using Nancy.Testing;
    using NUnit.Framework;
    using DependencyInjection;

    [TestFixture]
    public class InfoModuleGetTest
    {
        Browser         _browser;
        BrowserResponse _browserResponse;

        public InfoModuleGetTest()
        {
            _browser = new Browser(new CustomBootstrapper(new RegisterTypes().ForCore()));
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {           
            _browserResponse = _browser.Get("/info", with =>
            {
                with.HttpRequest();
            });
        }

        [Test, Description("Test that Core.InfoModule response to an HTTP [GET] request is not null"), Category("Core.Test.InfoModuleGetTest")]
        public void ResponseIsNotNullTest()
        {
            Assert.That(_browserResponse, Is.Not.Null);
        }

        [Test, Description("Test that Core.InfoModule responds to an HTTP [GET] request with application+text"), Category("Core.Test.InfoModuleGetTest")]
        public void ResponseContentTypeIsTextTest()
        {
            Assert.AreEqual("text/html", _browserResponse.ContentType);
        }

        [Test, Description("Test that Core.InfoModule responds to an HTTP [GET] request with a success, 'OK' status code"), Category("Core.Test.InfoModuleGetTest")]
        public void ResponseStatusCodeIsOkTest()
        {
            Assert.AreEqual(HttpStatusCode.OK, _browserResponse.StatusCode);// the main route now redirects to login
        }

        [Test, Description("Test that Core.InfoModule responds to an HTTP [GET] request as expected for the currently executing environment"), Category("Core.Test.InfoModuleGetTest")]
        public void ResponseContentByConfigurationTest()
        {
            string responseText = _browserResponse.Body.AsString();
#if DEBUG
            Assert.AreEqual("Core's Info: Vader", responseText);
#else
            Assert.AreEqual("Core's Info: Yoda", _browserResponse.Body);
#endif
        }
    }
}
