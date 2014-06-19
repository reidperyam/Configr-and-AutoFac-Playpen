#r "Common.dll"

using Common;

Configuration coreConfiguration = new Configuration();// type is defined in Common

coreConfiguration.Info = "From Core.Release.csx";
coreConfiguration.AuthenticationEnabled = true;

Add("CoreConfiguration", coreConfiguration);