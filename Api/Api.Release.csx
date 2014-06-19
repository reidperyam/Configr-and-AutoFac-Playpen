#r "Common.dll"

using Common;

Configuration apiConfiguration = new Configuration();// type is defined in Common

apiConfiguration.Info = "From Api.Release.csx";
apiConfiguration.AuthenticationEnabled = true;

Add("ApiConfiguration", apiConfiguration);