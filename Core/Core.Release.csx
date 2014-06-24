#r "bin/Common.dll"

using Common;

CoreConfiguration coreConfiguration = new CoreConfiguration();// type is defined in Common

coreConfiguration.Info = "From Core.Release.csx";
coreConfiguration.AuthenticationEnabled = true;

Add("CoreConfiguration", coreConfiguration);