namespace Utilities
{
    using Utilities.Contract;

    public class Info : IInfo
    {
        string _info;

        public Info(string info)
        {
            _info = info;
        }

        string IInfo.Info
        {
            get { return _info; }
        }
    }
}
