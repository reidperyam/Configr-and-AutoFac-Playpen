#r "Common.dll"

using Common;

CommonConfiguration config = new CommonConfiguration();// type is defined in SalesApplication.Common

config.Info  = "Vader";

Add("Configuration", config);