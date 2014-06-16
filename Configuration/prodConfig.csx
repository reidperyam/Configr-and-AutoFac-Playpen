#r "Common.dll"

using Common;

CommonConfiguration config = new CommonConfiguration();// type is defined in SalesApplication.Common

string guid = Guid.NewGuid().ToString();

config.Info  = guid;

Add("Configuration", config);