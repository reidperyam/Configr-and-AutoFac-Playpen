#r "Common.dll"

using Common;

Configuration coreConfiguration = new Configuration();// type is defined in Common

coreConfiguration.Info = "From Core.Debug.csx";
coreConfiguration.AuthenticationEnabled = false;

Add("CoreConfiguration", coreConfiguration);