namespace Api.Test
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    [TestFixture]
    public class ConfigrTest
    {
        List<string> GetScriptFiles()
        {
            return new List<string>()
            {//output by Configuration.csproj
                "devConfig.csx",
                "prodConfig.csx"
            };
        }

        string   _appDomainBaseDirectory;

        public ConfigrTest()
        {
            _appDomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        [Test, Description("Test that dependant Configr .csx file can be located in the AppDomain.CurrentDomain.BaseDirectory"), Category("Api.Test.ConfigrTest")]
        public void ScriptFileIsPresent([ValueSource("GetScriptFiles")]string scriptFile)
        {
            string errorMessage = string.Format("Required script file not present: '{0}' in directory '{1}'", scriptFile, _appDomainBaseDirectory);
            string[] files = Directory.GetFiles(_appDomainBaseDirectory, scriptFile, SearchOption.TopDirectoryOnly);

            Assert.That(files.Count() == 1, errorMessage);
            Assert.IsTrue(files[0].Contains(scriptFile), errorMessage);
        }
    }
}
