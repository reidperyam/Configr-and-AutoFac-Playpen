namespace Utilities
{
    using Utilities.Contract;

    public class CoreInfo : IInfo
    {
        IInfo _iinfo;

        public CoreInfo(IInfo iinfo)
        {
            _iinfo = iinfo;
        }

        string IInfo.Info
        {
            get { return "Core's Info: " + _iinfo.Info; }
        }
    }
}
