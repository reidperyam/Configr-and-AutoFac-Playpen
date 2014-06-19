namespace Utilities
{
    using Utilities.Contract;

    public class Info : IInfo
    {
        string _info;
        bool _authenticationEnabled;

        public Info(string info, bool authenticationEnabled)
        {
            _info = info;
            _authenticationEnabled = authenticationEnabled;
        }

        string IInfo.Info
        {
            get { return _info + " AuthenticationEnabled: " + _authenticationEnabled; }
        }
    }
}
