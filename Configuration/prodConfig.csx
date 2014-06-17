#r "Common.dll"

using Common;

CommonConfiguration config = new CommonConfiguration();// type is defined in SalesApplication.Common

config.Info  = "Yoda";

Add("Configuration", config);