namespace Core
{
    using Nancy;
    using Utilities.Contract;

    public class InfoModule : NancyModule
    {
        public InfoModule() { }

        public InfoModule(IInfo iinfo)
        {
            Get["/info"] = _ =>
            {
                return iinfo.Info;
            };
        }
    }
}