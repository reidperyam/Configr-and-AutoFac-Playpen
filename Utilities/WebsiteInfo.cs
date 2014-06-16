namespace Utilities
{
    using Utilities.Contract;

    public class WebsiteInfo : IInfo
    {
        IInfo _iinfo;

        public WebsiteInfo(IInfo iinfo)
        {
            _iinfo = iinfo;
        }

        string IInfo.Info
        {
            get { return "Website's Info: " + _iinfo.Info; }
        }
    }
}
