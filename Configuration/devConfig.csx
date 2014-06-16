#r "Common.dll"

using Common;

CommonConfiguration config = new CommonConfiguration();// type is defined in SalesApplication.Common

string machineName = System.Environment.MachineName;

config.Info  = machineName;

Add("Configuration", config);