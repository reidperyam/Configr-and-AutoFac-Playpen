namespace Utilities
{
    using Utilities.Contract;

    public class ApiInfo : IInfo
    {
        IInfo _iinfo;

        public ApiInfo(IInfo iinfo)
        {
            _iinfo = iinfo;
        }

        string IInfo.Info
        {
            get { return "Api's Info: " + _iinfo.Info; }
        }
    }
}
