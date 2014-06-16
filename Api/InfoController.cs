namespace Api
{
    using System.Web.Http;
    using Utilities.Contract;

    [RoutePrefix("api")]
    public class InfoController : ApiController
    {
        private readonly IInfo _iinfo;

        public InfoController()
        { }

        public InfoController(IInfo iinfo)
        {
            _iinfo = iinfo;
        }

        [Route("info")]
        public string GetInfo()
        {
            return _iinfo.Info;
        }
    }
}
