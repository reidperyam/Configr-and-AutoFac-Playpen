namespace Core
{
    using Nancy;
    using Utilities.Contract;

    public class DefaultModule : NancyModule
    {
        IInfo _iinfo;

        public DefaultModule()
        { }

        public DefaultModule(IInfo iinfo)
        {
            _iinfo = iinfo;

            Get["/hellonancy"] = _ => "Hello World!";
            Get["/info"]       = _ => _iinfo.Info;
        }
    }
}