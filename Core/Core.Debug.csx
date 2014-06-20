#r "Common.dll"

using Common;

CoreConfiguration coreConfiguration = new CoreConfiguration();// type is defined in Common

coreConfiguration.Info = "From Core.Debug.csx";
coreConfiguration.AuthenticationEnabled = false;

Add("CoreConfiguration", coreConfiguration);